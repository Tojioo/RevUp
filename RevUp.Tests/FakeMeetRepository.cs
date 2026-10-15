using RevUp.Data;
using RevUp.Models;

namespace RevUp.Tests;

/// <summary>Keeps meets in a list, so service tests run without a database.</summary>
internal sealed class FakeMeetRepository : IMeetRepository
{
	public List<Meet> Meets { get; } = [];

	public Task<IReadOnlyList<Meet>> GetAllAsync(CancellationToken cancellationToken = default) =>
		Task.FromResult<IReadOnlyList<Meet>>(Meets.ToList());

	public Task AddAsync(Meet meet, CancellationToken cancellationToken = default)
	{
		Meets.Add(meet);
		return Task.CompletedTask;
	}
}