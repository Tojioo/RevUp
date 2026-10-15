using LinqToDB;
using Microsoft.Data.Sqlite;
using RevUp.Data;

namespace RevUp.IntegrationTests;

/// <summary>Gives every test its own copy of the checked-in snapshot database, so tests never share or seed data.</summary>
public abstract class DatabaseTestBase : IAsyncLifetime
{
	private readonly string _databasePath = Path.Combine(Path.GetTempPath(), $"revup-test-{Guid.NewGuid():N}.db");

	// Because a DataConnection only opens on first use, it can exist before InitializeAsync has copied the file.
	protected DatabaseTestBase()
	{
		Options = RevUpDatabase.CreateOptions($"Data Source={_databasePath}");
		Context = new RevUpDataConnection(Options);
	}

	protected DataOptions Options { get; }

	protected RevUpDataConnection Context { get; }

	public async Task InitializeAsync()
	{
		var snapshotPath = Path.Combine(AppContext.BaseDirectory, "Snapshot", "revup.db");
		await using (var snapshot = File.OpenRead(snapshotPath))
		{
			await using var copy = File.Create(_databasePath);
			await snapshot.CopyToAsync(copy);
		}
	}

	public async Task DisposeAsync()
	{
		await Context.DisposeAsync();

		// Because pooled SQLite connections keep the file open, which would make the delete fail.
		SqliteConnection.ClearAllPools();
		File.Delete(_databasePath);
	}
}