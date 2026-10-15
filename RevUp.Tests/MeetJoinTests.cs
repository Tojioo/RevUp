using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public class MeetJoinTests
{
	private readonly FakeMeetRepository _repository = new();
	private readonly MeetService _service;

	public MeetJoinTests() =>
		_service = new MeetService(_repository, new FakeTimeProvider(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero)));

	[Fact]
	public async Task JoiningAPublicMeetIsSavedAndRevealsTheLocation()
	{
		var meet = MeetFactory.Create(isPublic: true);
		_repository.Meets.Add(meet);

		var result = await _service.JoinAsync(meet.Id, "Noah");

		Assert.Equal(new JoinResult(JoinStatus.Joined, meet.Location), result);
		Assert.Equal([meet.Id], _repository.Updates);
		Assert.Equal(1, (await _service.GetMeetDetailsAsync(meet.Id))!.AttendeeCount);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData("Ein Name, der deutlich länger als vierzig Zeichen ist")]
	public async Task AMissingOrTooLongNameIsRejected(string name)
	{
		var meet = MeetFactory.Create(isPublic: true);
		_repository.Meets.Add(meet);

		var result = await _service.JoinAsync(meet.Id, name);

		Assert.Equal(new JoinResult(JoinStatus.InvalidName), result);
		Assert.Empty(_repository.Updates);
	}

	[Fact]
	public async Task AnUnknownMeetCannotBeJoined() =>
		Assert.Equal(new JoinResult(JoinStatus.NotFound), await _service.JoinAsync(Guid.NewGuid(), "Noah"));

	[Fact]
	public async Task AFullMeetIsNotSaved()
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 2, "Aylin", "Livio");
		_repository.Meets.Add(meet);

		var result = await _service.JoinAsync(meet.Id, "Noah");

		Assert.Equal(JoinStatus.Full, result.Status);
		Assert.Empty(_repository.Updates);
	}

	[Fact]
	public async Task TheDetailsShowTheMeetWithoutItsLocation()
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 20, "Aylin");
		_repository.Meets.Add(meet);

		var details = await _service.GetMeetDetailsAsync(meet.Id);

		Assert.Equal(
			new MeetDetails(meet.Id, meet.Title, meet.Theme, MeetTimeWindow.Format(meet), meet.OrganizerName, meet.MaxVolumeDb, true, "Öffentlich", "Ort nach Beitritt", 1, 20),
			details);
		Assert.DoesNotContain(typeof(MeetDetails).GetProperties(), property => property.Name == nameof(Meet.Location));
	}

	[Fact]
	public async Task AnUnknownMeetHasNoDetails() => Assert.Null(await _service.GetMeetDetailsAsync(Guid.NewGuid()));
}