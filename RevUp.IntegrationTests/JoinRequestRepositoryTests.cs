using LinqToDB.Async;
using RevUp.Data;
using RevUp.Models;

namespace RevUp.IntegrationTests;

public class JoinRequestRepositoryTests : DatabaseTestBase
{
	[Fact]
	public async Task AnAddedRequestIsStoredAndFoundForItsMeet()
	{
		var meets = await Context.Meets.ToListAsync();
		var request = new JoinRequest
		{
			MeetId = meets[0].Id,
			ParticipantName = "Noah",
			CreatedAt = new DateTime(2027, 5, 1, 17, 30, 0),
		};
		var repository = new JoinRequestRepository(Options);

		await repository.AddAsync(request);

		Assert.Equivalent(request, Assert.Single(await repository.GetForMeetAsync(meets[0].Id)));
		Assert.Empty(await repository.GetForMeetAsync(meets[1].Id));
	}

	[Fact]
	public async Task ADecidedRequestIsFoundByIdWithItsNewStatus()
	{
		var meets = await Context.Meets.ToListAsync();
		var request = new JoinRequest { MeetId = meets[0].Id, ParticipantName = "Noah" };
		var repository = new JoinRequestRepository(Options);
		await repository.AddAsync(request);
		request.Status = RequestStatus.Approved;

		await repository.UpdateAsync(request);

		Assert.Equal(RequestStatus.Approved, (await repository.GetByIdAsync(request.Id))!.Status);
		Assert.Null(await repository.GetByIdAsync(Guid.NewGuid()));
	}
}