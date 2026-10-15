using LinqToDB;
using RevUp.Models;

namespace RevUp.Data;

public sealed class MeetRepository(DataOptions options) : IMeetRepository
{
	public Task<IReadOnlyList<Meet>> GetAllAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task AddAsync(Meet meet, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}