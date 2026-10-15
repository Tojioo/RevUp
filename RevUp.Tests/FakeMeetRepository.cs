using RevUp.Data;
using RevUp.Models;

namespace RevUp.Tests;

/// <summary>Keeps meets in a list, so service tests run without a database.</summary>
internal sealed class FakeMeetRepository : IMeetRepository
{
	public List<Meet> Meets { get; } = [];

	public List<Guid> Updates { get; } = [];

	public Task<IReadOnlyList<Meet>> GetAllAsync(CancellationToken cancellationToken = default) =>
		Task.FromResult<IReadOnlyList<Meet>>(Meets.ToList());

	public Task<Meet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
		Task.FromResult(Meets.SingleOrDefault(meet => meet.Id == id));

	public Task UpdateAsync(Meet meet, CancellationToken cancellationToken = default)
	{
		Updates.Add(meet.Id);
		return Task.CompletedTask;
	}

	public Task AddAsync(Meet meet, CancellationToken cancellationToken = default)
	{
		Meets.Add(meet);
		return Task.CompletedTask;
	}
}