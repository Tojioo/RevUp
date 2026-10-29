using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public sealed class MeetDecisionTests : IDisposable
{
	private readonly FakeMeetRepository _repository = new();
	private readonly FakeJoinRequestRepository _requests = new();
	private readonly MeetChangeNotifier _notifier = new();
	private readonly RecordingObserver _observer = new();
	private readonly MeetService _service;

	public MeetDecisionTests()
	{
		_service = new MeetService(_repository, _requests, _notifier, new FakeTimeProvider(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero)));
		_notifier.Attach(_observer);
	}

	public void Dispose() => _service.Dispose();

	[Fact]
	public async Task AnAcceptedParticipantJoinsAndThenSeesTheLocation()
	{
		var meet = AddPrivateMeet(maxAttendees: 20);
		var request = await RequestAsync(meet, "Noah");

		var decision = await _service.DecideAsync(request.Id, approve: true);

		Assert.Equal(DecisionStatus.Approved, decision);
		Assert.Equal(RequestStatus.Approved, request.Status);
		Assert.Equal(["Noah"], meet.AttendeeNames);
		Assert.Contains(meet.Id, _repository.Updates);
		Assert.Equal(new JoinResult(JoinStatus.AlreadyJoined, meet.Location), await _service.JoinAsync(meet.Id, "Noah"));
		Assert.Empty(await _service.GetPendingRequestsAsync(meet.Id));
	}

	[Fact]
	public async Task ARejectedParticipantNeverSeesTheLocation()
	{
		var meet = AddPrivateMeet(maxAttendees: 20);
		var request = await RequestAsync(meet, "Noah");

		var decision = await _service.DecideAsync(request.Id, approve: false);

		Assert.Equal(DecisionStatus.Rejected, decision);
		Assert.Equal(RequestStatus.Rejected, request.Status);
		Assert.Empty(meet.AttendeeNames);
		Assert.Equal(new JoinResult(JoinStatus.Rejected), await _service.JoinAsync(meet.Id, "Noah"));
		Assert.Single(_requests.Requests);
	}

	[Fact]
	public async Task AFullMeetKeepsTheRequestOpen()
	{
		var meet = AddPrivateMeet(maxAttendees: 1);
		var request = await RequestAsync(meet, "Noah");
		meet.AttendeeNames.Add("Aylin");

		var decision = await _service.DecideAsync(request.Id, approve: true);

		Assert.Equal(DecisionStatus.Full, decision);
		Assert.Equal(RequestStatus.Pending, request.Status);
		Assert.DoesNotContain("Noah", meet.AttendeeNames);
	}

	[Fact]
	public async Task AnUnknownOrAlreadyDecidedRequestIsNotFound()
	{
		var meet = AddPrivateMeet(maxAttendees: 20);
		var request = await RequestAsync(meet, "Noah");
		await _service.DecideAsync(request.Id, approve: false);

		Assert.Equal(DecisionStatus.NotFound, await _service.DecideAsync(request.Id, approve: true));
		Assert.Equal(DecisionStatus.NotFound, await _service.DecideAsync(Guid.NewGuid(), approve: true));
	}

	[Fact]
	public async Task EveryChangeToAMeetIsReportedToItsObservers()
	{
		var privateMeet = AddPrivateMeet(maxAttendees: 20);
		var publicMeet = MeetFactory.Create(isPublic: true);
		_repository.Meets.Add(publicMeet);

		await _service.JoinAsync(publicMeet.Id, "Aylin");
		var request = await RequestAsync(privateMeet, "Noah");
		await _service.DecideAsync(request.Id, approve: true);
		var created = (await _service.CreateAsync(MeetDrafts.Valid())).Meet!;

		Assert.Equal([publicMeet.Id, privateMeet.Id, privateMeet.Id, created.Id], _observer.ChangedMeets);
	}

	[Fact]
	public async Task NothingIsReportedWhenNothingChanged()
	{
		var meet = AddPrivateMeet(maxAttendees: 20);
		await RequestAsync(meet, "Noah");
		_observer.ChangedMeets.Clear();

		await _service.JoinAsync(meet.Id, "Noah");
		await _service.JoinAsync(meet.Id, string.Empty);

		Assert.Empty(_observer.ChangedMeets);
	}

	private Meet AddPrivateMeet(int maxAttendees)
	{
		var meet = MeetFactory.Create(isPublic: false, maxAttendees);
		_repository.Meets.Add(meet);
		return meet;
	}

	private async Task<JoinRequest> RequestAsync(Meet meet, string name)
	{
		await _service.JoinAsync(meet.Id, name);
		return _requests.Requests.Single(request => request.MeetId == meet.Id && request.ParticipantName == name);
	}
}