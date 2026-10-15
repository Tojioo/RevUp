using RevUp.Models;

namespace RevUp.Data;

public interface IMeetRepository
{
	Task<IReadOnlyList<Meet>> GetAllAsync(CancellationToken cancellationToken = default);

	Task<Meet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task AddAsync(Meet meet, CancellationToken cancellationToken = default);

	Task UpdateAsync(Meet meet, CancellationToken cancellationToken = default);
}