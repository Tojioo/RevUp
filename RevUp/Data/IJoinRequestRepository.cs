using RevUp.Models;

namespace RevUp.Data;

public interface IJoinRequestRepository
{
	Task<IReadOnlyList<JoinRequest>> GetForMeetAsync(Guid meetId, CancellationToken cancellationToken = default);

	Task<JoinRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task AddAsync(JoinRequest request, CancellationToken cancellationToken = default);

	Task UpdateAsync(JoinRequest request, CancellationToken cancellationToken = default);
}