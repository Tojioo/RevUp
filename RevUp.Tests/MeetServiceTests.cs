using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public sealed class MeetServiceTests : IDisposable
{
	private readonly FakeMeetRepository _repository = new();
	private readonly MeetService _service;

	public MeetServiceTests() =>
		_service = new MeetService(_repository, new FakeTimeProvider(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero)));

	public void Dispose() => _service.Dispose();

	[Fact]
	public async Task AValidMeetIsSavedAndAppearsInTheList()
	{
		var draft = MeetDrafts.Valid();
		draft.Title = "  Sonntagsausfahrt  ";

		var result = await _service.CreateAsync(draft);

		Assert.True(result.Succeeded);
		Assert.Empty(result.Errors);
		var meet = Assert.Single(_repository.Meets);
		Assert.Equal(result.Meet!.Id, meet.Id);
		Assert.Equal("Sonntagsausfahrt", meet.Title);
		Assert.Equal(draft.Theme, meet.Theme);
		Assert.Equal(draft.Location, meet.Location);
		Assert.Equal(draft.OrganizerName, meet.OrganizerName);
		Assert.Equal(TimeMode.Fixed, meet.TimeMode);
		Assert.Equal(draft.StartTime, meet.StartTime);
		Assert.Equal(draft.EndTime, meet.EndTime);
		Assert.Equal(draft.MaxAttendees, meet.MaxAttendees);
		Assert.Equal(draft.MaxVolumeDb, meet.MaxVolumeDb);
		Assert.Equal(0, meet.AttendeeCount);
		Assert.Contains(await _service.GetUpcomingMeetsAsync(), item => item.Id == meet.Id);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task TheChosenVisibilityIsSaved(bool isPublic)
	{
		var draft = MeetDrafts.Valid();
		draft.IsPublic = isPublic;

		await _service.CreateAsync(draft);

		Assert.Equal(isPublic, Assert.Single(_repository.Meets).IsPublic);
	}

	[Fact]
	public void ANewDraftIsPublicLikeInThePrototype() => Assert.True(new MeetDraft().IsPublic);

	[Fact]
	public async Task AnInvalidMeetIsNotSaved()
	{
		var draft = MeetDrafts.Valid();
		draft.Title = string.Empty;

		var result = await _service.CreateAsync(draft);

		Assert.False(result.Succeeded);
		Assert.Contains(result.Errors, error => error.Field == nameof(MeetDraft.Title));
		Assert.Empty(_repository.Meets);
	}
}