using LinqToDB;
using LinqToDB.Async;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.Mapping;
using RevUp.Models;

namespace RevUp.Data;

public static class RevUpDatabase
{
	private static readonly MappingSchema _mappingSchema = RevUpMappingSchema.Create();

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
}