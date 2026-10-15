using LinqToDB;
using LinqToDB.Async;
using RevUp.Models;

namespace RevUp.Data;

/// <summary>Opens a short-lived connection per call, because a DataConnection must not be shared between Blazor circuits.</summary>
public sealed class MeetRepository(DataOptions options) : IMeetRepository
{
	public async Task<IReadOnlyList<Meet>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		await using var db = new RevUpDataConnection(options);
		return await db.Meets.ToListAsync(cancellationToken);
	}

	public async Task<Meet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		await using var db = new RevUpDataConnection(options);
		return await db.Meets.FirstOrDefaultAsync(meet => meet.Id == id, cancellationToken);
	}

	public async Task UpdateAsync(Meet meet, CancellationToken cancellationToken = default)
	{
		await using var db = new RevUpDataConnection(options);
		await db.UpdateAsync(meet, token: cancellationToken);
	}

	public async Task AddAsync(Meet meet, CancellationToken cancellationToken = default)
	{
		await using var db = new RevUpDataConnection(options);
		await db.InsertAsync(meet, token: cancellationToken);
	}
}