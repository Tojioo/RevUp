using LinqToDB;
using LinqToDB.Async;
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
}