using RevUp.Data;

namespace RevUp.Tests;

public class MeetSeederTests
{
	[Fact]
	public void SampleMeetsTakePlaceOnTheComingWeekend()
	{
		var monday = new DateTime(2026, 10, 12);

		var meets = MeetSeeder.CreateSampleMeets(monday);

		Assert.Equal(3, meets.Count);
		Assert.All(meets, meet => Assert.InRange(meet.StartTime.Date, new DateTime(2026, 10, 17), new DateTime(2026, 10, 18)));
	}
}