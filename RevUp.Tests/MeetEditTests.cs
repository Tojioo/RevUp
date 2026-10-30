using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public sealed class MeetEditTests : IDisposable
{
	private readonly FakeTimeProvider _clock = new(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero));
	private readonly FakeMeetRepository _repository = new();
	private readonly MeetChangeNotifier _notifier = new();
	private readonly RecordingObserver _observer = new();
	private readonly MeetService _service;

	public MeetEditTests()
	{
		_service = new MeetService(_repository, new FakeJoinRequestRepository(), _notifier, _clock);
		_notifier.Attach(_observer);
	}

	public void Dispose() => _service.Dispose();

	[Fact]
	public async Task TheEditFormStartsWithTheCurrentValues()
	{
		var meet = AddMeet("Aylin");

		var draft = await _service.GetDraftForEditAsync(meet.Id);

		Assert.NotNull(draft);
		Assert.Equal(meet.Title, draft.Title);
		Assert.Equal(meet.Theme, draft.Theme);
		Assert.Equal(meet.Location, draft.Location);
		Assert.Equal(meet.OrganizerName, draft.OrganizerName);
		Assert.Equal(meet.TimeMode, draft.TimeMode);
		Assert.Equal(meet.StartTime, draft.StartTime);
		Assert.Equal(meet.EndTime, draft.EndTime);
		Assert.Equal(meet.MaxAttendees, draft.MaxAttendees);
		Assert.Equal(meet.MaxVolumeDb, draft.MaxVolumeDb);
		Assert.Equal(meet.IsPublic, draft.IsPublic);
		Assert.Null(await _service.GetDraftForEditAsync(Guid.NewGuid()));
	}

	[Fact]
	public async Task AnEditIsSavedWithItsTimeAndReportedToObservers()
	{
		var meet = AddMeet("Aylin");
		_clock.Advance(TimeSpan.FromMinutes(30));
		var draft = (await _service.GetDraftForEditAsync(meet.Id))!;
		draft.Title = "  Neuer Titel  ";
		draft.Location = "Neuer Treffpunkt";

		var result = await _service.UpdateAsync(meet.Id, draft);

		Assert.True(result!.Succeeded);
		Assert.Equal("Neuer Titel", meet.Title);
		Assert.Equal("Neuer Treffpunkt", meet.Location);
		Assert.Equal(["Aylin"], meet.AttendeeNames);
		Assert.Equal(MeetDrafts.Now.AddMinutes(30), meet.ChangedAt);
		Assert.Contains(meet.Id, _repository.Updates);
		Assert.Equal([meet.Id], _observer.ChangedMeets);
		Assert.Equal(MeetDrafts.Now.AddMinutes(30), (await _service.GetMeetDetailsAsync(meet.Id))!.ChangedAt);
	}

	[Fact]
	public async Task AnInvalidEditChangesNothing()
	{
		var meet = AddMeet();
		var draft = (await _service.GetDraftForEditAsync(meet.Id))!;
		draft.Title = string.Empty;

		var result = await _service.UpdateAsync(meet.Id, draft);

		Assert.False(result!.Succeeded);
		Assert.Contains(result.Errors, error => error.Field == nameof(MeetDraft.Title));
		Assert.Equal("Treff", meet.Title);
		Assert.Empty(_repository.Updates);
		Assert.Empty(_observer.ChangedMeets);
	}

	[Fact]
	public async Task TheLimitCannotDropBelowThePeopleAlreadyIn()
	{
		var meet = AddMeet("Aylin", "Livio", "Noah");
		var draft = (await _service.GetDraftForEditAsync(meet.Id))!;
		draft.MaxAttendees = 2;

		var result = await _service.UpdateAsync(meet.Id, draft);

		Assert.Equal([new ValidationError(nameof(MeetDraft.MaxAttendees), "Mindestens 3, so viele sind schon dabei.")], result!.Errors);
		Assert.Equal(20, meet.MaxAttendees);
	}

	[Fact]
	public async Task TheVisibilityStaysAsItWasCreated()
	{
		var meet = AddMeet();
		var draft = (await _service.GetDraftForEditAsync(meet.Id))!;
		draft.IsPublic = !meet.IsPublic;

		await _service.UpdateAsync(meet.Id, draft);

		Assert.True(meet.IsPublic);
	}

	[Fact]
	public async Task ACancelledMeetIsMarkedEverywhereAndCannotBeJoined()
	{
		var meet = AddMeet();

		var cancelled = await _service.CancelAsync(meet.Id);

		Assert.True(cancelled);
		Assert.True(meet.IsCancelled);
		Assert.Equal([meet.Id], _observer.ChangedMeets);
		Assert.True(Assert.Single(await _service.GetUpcomingMeetsAsync()).IsCancelled);
		Assert.True((await _service.GetMeetDetailsAsync(meet.Id))!.IsCancelled);
		Assert.Equal(new JoinResult(JoinStatus.Cancelled), await _service.JoinAsync(meet.Id, "Noah"));
		Assert.Empty(meet.AttendeeNames);
	}

	[Fact]
	public async Task AnUnknownMeetCannotBeEditedOrCancelled()
	{
		Assert.Null(await _service.UpdateAsync(Guid.NewGuid(), MeetDrafts.Valid()));
		Assert.False(await _service.CancelAsync(Guid.NewGuid()));
	}

	private Meet AddMeet(params string[] attendees)
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 20, attendees);
		_repository.Meets.Add(meet);
		return meet;
	}
}