using RevUp.Models;

namespace RevUp.Data;

public interface IMeetRepository
{
	Task<IReadOnlyList<Meet>> GetAllAsync(CancellationToken cancellationToken = default);

	Task AddAsync(Meet meet, CancellationToken cancellationToken = default);
}