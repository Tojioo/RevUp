using LinqToDB.Async;
using RevUp.Data;
using RevUp.Models;

namespace RevUp.IntegrationTests;

public class MeetRepositoryTests : DatabaseTestBase
{
	[Fact]
	public async Task TheStoredMeetsAreLoadedWithTheirAttendees()
	{
		var meets = await new MeetRepository(Options).GetAllAsync();

		Assert.Equal(3, meets.Count);
		var tuning = Assert.Single(meets, meet => meet.Title == "Sonntagsausfahrt Tuning-Treff");
		Assert.True(tuning.IsPublic);
		Assert.Equal(TimeMode.Fixed, tuning.TimeMode);
		Assert.NotNull(tuning.EndTime);
		Assert.Equal(12, tuning.AttendeeCount);
		var openStart = Assert.Single(meets, meet => meet.TimeMode == TimeMode.OpenStart);
		Assert.False(openStart.IsPublic);
		Assert.Null(openStart.EndTime);
	}

	[Fact]
	public async Task AnAddedMeetIsStoredWithAllItsFields()
	{
		var meet = new Meet
		{
			Title = "Herbst-Treff",
			Theme = "JDM",
			Location = "Parkplatz Seeufer, Luzern",
			OrganizerName = "Nicolas",
			IsPublic = false,
			TimeMode = TimeMode.Fixed,
			StartTime = new DateTime(2027, 5, 1, 18, 0, 0),
			EndTime = new DateTime(2027, 5, 1, 22, 0, 0),
			MaxAttendees = 25,
			MaxVolumeDb = 85,
			AttendeeNames = ["Aylin", "Noah"],
		};

		await new MeetRepository(Options).AddAsync(meet);

		var stored = await Context.Meets.SingleAsync(candidate => candidate.Id == meet.Id);
		Assert.Equivalent(meet, stored);
	}

	[Fact]
	public async Task AMeetIsFoundByItsId()
	{
		var repository = new MeetRepository(Options);
		var expected = (await repository.GetAllAsync())[0];

		var found = await repository.GetByIdAsync(expected.Id);

		Assert.Equivalent(expected, found);
		Assert.Null(await repository.GetByIdAsync(Guid.NewGuid()));
	}

	[Fact]
	public async Task AnUpdatedAttendeeListIsStored()
	{
		var repository = new MeetRepository(Options);
		var meet = (await repository.GetAllAsync())[0];
		meet.AttendeeNames.Add("Noah");

		await repository.UpdateAsync(meet);

		var stored = await Context.Meets.SingleAsync(candidate => candidate.Id == meet.Id);
		Assert.Equal(meet.AttendeeNames, stored.AttendeeNames);
	}
}