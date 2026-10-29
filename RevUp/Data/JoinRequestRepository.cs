using LinqToDB;
using RevUp.Models;

namespace RevUp.Data;

public sealed class JoinRequestRepository(DataOptions options) : IJoinRequestRepository
{
	public Task<IReadOnlyList<JoinRequest>> GetForMeetAsync(Guid meetId, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task AddAsync(JoinRequest request, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}