using LinqToDB;

namespace RevUp.Data;

public static class RevUpDatabase
{
	public static DataOptions CreateOptions(string connectionString) => throw new NotImplementedException();

	/// <summary>Creates missing tables and fills an empty meet table with sample meets.</summary>
	public static Task InitializeAsync(DataOptions options, TimeProvider clock, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}