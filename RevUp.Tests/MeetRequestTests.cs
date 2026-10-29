using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public sealed class MeetRequestTests : IDisposable
{
	private readonly FakeTimeProvider _clock = new(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero));
	private readonly FakeMeetRepository _repository = new();
	private readonly FakeJoinRequestRepository _requests = new();
	private readonly MeetService _service;

	public MeetRequestTests() => _service = new MeetService(_repository, _requests, _clock);

	public void Dispose() => _service.Dispose();

	[Fact]
	public async Task JoiningAPrivateMeetStoresAPendingRequestWithoutRevealingTheLocation()
	{
		var meet = AddMeet(isPublic: false);

		var result = await _service.JoinAsync(meet.Id, "  Noah  ");

		Assert.Equal(new JoinResult(JoinStatus.RequestPending), result);
		var request = Assert.Single(_requests.Requests);
		Assert.Equal(meet.Id, request.MeetId);
		Assert.Equal("Noah", request.ParticipantName);
		Assert.Equal(RequestStatus.Pending, request.Status);
		Assert.Equal(MeetDrafts.Now, request.CreatedAt);
		Assert.Empty(meet.AttendeeNames);
		Assert.Empty(_repository.Updates);
	}

	[Fact]
	public async Task AskingAgainWithTheSameNameKeepsASingleRequest()
	{
		var meet = AddMeet(isPublic: false);

		await _service.JoinAsync(meet.Id, "Noah");
		var second = await _service.JoinAsync(meet.Id, "noah");

		Assert.Equal(JoinStatus.RequestPending, second.Status);
		Assert.Single(_requests.Requests);
	}

	[Fact]
	public async Task APublicMeetCreatesNoRequests()
	{
		var meet = AddMeet(isPublic: true);

		await _service.JoinAsync(meet.Id, "Noah");

		Assert.Empty(_requests.Requests);
	}

	[Fact]
	public async Task TheOrganizerSeesOpenRequestsOldestFirst()
	{
		var meet = AddMeet(isPublic: false);
		await _service.JoinAsync(meet.Id, "Sina");
		_clock.Advance(TimeSpan.FromMinutes(5));
		await _service.JoinAsync(meet.Id, "Noah");
		_requests.Requests.Add(new JoinRequest { MeetId = meet.Id, ParticipantName = "Elif", Status = RequestStatus.Rejected });

		var pending = await _service.GetPendingRequestsAsync(meet.Id);

		Assert.Equal(["Sina", "Noah"], pending.Select(request => request.ParticipantName));
		Assert.Equal(MeetDrafts.Now, pending[0].CreatedAt);
	}

	private Meet AddMeet(bool isPublic)
	{
		var meet = MeetFactory.Create(isPublic);
		_repository.Meets.Add(meet);
		return meet;
	}
}