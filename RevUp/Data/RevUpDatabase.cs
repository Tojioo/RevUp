using LinqToDB;
using LinqToDB.Async;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.Mapping;
using RevUp.Models;

namespace RevUp.Data;

public static class RevUpDatabase
{
	private static readonly MappingSchema _mappingSchema = RevUpMappingSchema.Create();

	// Columns added after the first release; same SQL types as linq2db's CreateTable uses for them.
	private static readonly (string Name, string Definition)[] _addedMeetColumns =
	[
		(nameof(Meet.IsCancelled), "Bit NOT NULL DEFAULT 0"),
		(nameof(Meet.ChangedAt), "DateTime2 NULL"),
	];

	public static DataOptions CreateOptions(string connectionString) =>
		new DataOptions()
			.UseSQLite(connectionString, SQLiteProvider.Microsoft)
			.UseMappingSchema(_mappingSchema);

	/// <summary>Creates missing tables and fills an empty meet table with sample meets.</summary>
	public static async Task InitializeAsync(DataOptions options, TimeProvider clock, CancellationToken cancellationToken = default)
	{
		await using var db = new RevUpDataConnection(options);
		await db.CreateTableAsync<Meet>(tableOptions: TableOptions.CreateIfNotExists, token: cancellationToken);
		await db.CreateTableAsync<JoinRequest>(tableOptions: TableOptions.CreateIfNotExists, token: cancellationToken);
		await AddMissingMeetColumnsAsync(db, cancellationToken);
		if (await db.Meets.AnyAsync(cancellationToken))
		{
			return;
		}

		await using var transaction = await db.BeginTransactionAsync(cancellationToken);
		foreach (var meet in MeetSeeder.CreateSampleMeets(clock.GetLocalNow().Date))
		{
			await db.InsertAsync(meet, token: cancellationToken);
		}

		await transaction.CommitAsync(cancellationToken);
	}

	// Because CreateTableAsync leaves an existing table as it is, databases from before a new column need it added here.
	private static async Task AddMissingMeetColumnsAsync(RevUpDataConnection db, CancellationToken cancellationToken)
	{
		var existing = await new CommandInfo(db, "SELECT name FROM pragma_table_info('Meets')").QueryToListAsync<string>(cancellationToken);
		foreach (var (name, definition) in _addedMeetColumns.Where(column => !existing.Contains(column.Name)))
		{
			await new CommandInfo(db, $"ALTER TABLE [Meets] ADD COLUMN [{name}] {definition}").ExecuteAsync(cancellationToken);
		}
	}
}