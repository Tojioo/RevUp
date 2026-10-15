using LinqToDB;
using Microsoft.Data.Sqlite;
using RevUp.Data;

namespace RevUp.IntegrationTests;

/// <summary>Gives every test its own copy of the checked-in snapshot database, so tests never share or seed data.</summary>
public abstract class DatabaseTestBase : IAsyncLifetime
{
	private readonly string _databasePath = Path.Combine(Path.GetTempPath(), $"revup-test-{Guid.NewGuid():N}.db");

	protected DataOptions Options { get; private set; } = null!;

	protected RevUpDataConnection Context { get; private set; } = null!;

	public async Task InitializeAsync()
	{
		var snapshotPath = Path.Combine(AppContext.BaseDirectory, "Snapshot", "revup.db");
		await using (var snapshot = File.OpenRead(snapshotPath))
		{
			await using var copy = File.Create(_databasePath);
			await snapshot.CopyToAsync(copy);
		}

		Options = RevUpDatabase.CreateOptions($"Data Source={_databasePath}");
		Context = new RevUpDataConnection(Options);
	}

	public async Task DisposeAsync()
	{
		if (Context is not null)
		{
			await Context.DisposeAsync();
		}

		// Because pooled SQLite connections keep the file open, which would make the delete fail.
		SqliteConnection.ClearAllPools();
		File.Delete(_databasePath);
	}
}