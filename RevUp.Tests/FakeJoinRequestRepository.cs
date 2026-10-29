using RevUp.Data;
using RevUp.Models;

namespace RevUp.Tests;

/// <summary>Keeps join requests in a list, so service tests run without a database.</summary>
internal sealed class FakeJoinRequestRepository : IJoinRequestRepository
{
	public List<JoinRequest> Requests { get; } = [];

	public Task<IReadOnlyList<JoinRequest>> GetForMeetAsync(Guid meetId, CancellationToken cancellationToken = default) =>
		Task.FromResult<IReadOnlyList<JoinRequest>>(Requests.Where(request => request.MeetId == meetId).ToList());

	public Task AddAsync(JoinRequest request, CancellationToken cancellationToken = default)
	{
		Requests.Add(request);
		return Task.CompletedTask;
	}
}