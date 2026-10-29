using LinqToDB;
using LinqToDB.Async;
using RevUp.Models;

namespace RevUp.Data;

public sealed class JoinRequestRepository(DataOptions options) : IJoinRequestRepository
{
	public async Task<IReadOnlyList<JoinRequest>> GetForMeetAsync(Guid meetId, CancellationToken cancellationToken = default)
	{
		await using var db = new RevUpDataConnection(options);
		return await db.JoinRequests.Where(request => request.MeetId == meetId).ToListAsync(cancellationToken);
	}

	public Task<JoinRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task UpdateAsync(JoinRequest request, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public async Task AddAsync(JoinRequest request, CancellationToken cancellationToken = default)
	{
		await using var db = new RevUpDataConnection(options);
		await db.InsertAsync(request, token: cancellationToken);
	}
}