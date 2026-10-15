#:project ../RevUp/RevUp.csproj

// Writes the database snapshot the integration tests restore. Run from the repository root: dotnet run --no-cache --file tools/create-snapshot.cs
using Microsoft.Data.Sqlite;
using RevUp.Data;

var path = Path.GetFullPath(args.Length > 0 ? args[0] : Path.Combine("RevUp.IntegrationTests", "Snapshot", "revup.db"));
File.Delete(path);
await RevUpDatabase.InitializeAsync(RevUpDatabase.CreateOptions($"Data Source={path}"), TimeProvider.System);
SqliteConnection.ClearAllPools();
Console.WriteLine($"Snapshot written to {path}");