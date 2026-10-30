using LinqToDB;
using LinqToDB.Async;
using LinqToDB.Data;
using RevUp.Data;
using RevUp.Models;

namespace RevUp.IntegrationTests;

public class RevUpDatabaseTests : DatabaseTestBase
{
	[Fact]
	public async Task ADatabaseWithMeetsIsNotSeededAgain()
	{
		await RevUpDatabase.InitializeAsync(Options, TimeProvider.System);

		Assert.Equal(3, await Context.Meets.CountAsync());
	}

	[Fact]
	public async Task MissingTablesAreCreatedAndFilledWithSampleMeets()
	{
		await Context.DropTableAsync<JoinRequest>();
		await Context.DropTableAsync<Meet>();

		await RevUpDatabase.InitializeAsync(Options, TimeProvider.System);

		Assert.Equal(3, await Context.Meets.CountAsync());
		Assert.Equal(0, await Context.JoinRequests.CountAsync());
	}

	[Fact]
	public async Task ADatabaseFromBeforeCancellingGetsTheNewColumns()
	{
		await Context.DropTableAsync<Meet>();
		await Context.ExecuteAsync(
			"CREATE TABLE [Meets] ([Id] Guid NOT NULL PRIMARY KEY, [Title] NVarChar(255), [Theme] NVarChar(255), [Location] NVarChar(255), "
			+ "[OrganizerName] NVarChar(255), [IsPublic] Bit NOT NULL, [TimeMode] INTEGER NOT NULL, [StartTime] DateTime2 NOT NULL, "
			+ "[EndTime] DateTime2 NULL, [MaxAttendees] INTEGER NOT NULL, [MaxVolumeDb] INTEGER NOT NULL, [AttendeeNames] NVarChar(255))");

		await RevUpDatabase.InitializeAsync(Options, TimeProvider.System);

		var meets = await Context.Meets.ToListAsync();
		Assert.Equal(3, meets.Count);
		Assert.All(meets, meet => Assert.False(meet.IsCancelled));
	}
}