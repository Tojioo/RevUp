using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public class MeetTimeWindowTests
{
	private static readonly DateTime Saturday = new(2026, 10, 17, 18, 0, 0);

	[Fact]
	public void AFixedMeetShowsDayAndTimeSpan() =>
		Assert.Equal("Sa, 17.10. · 18:00–22:00", MeetTimeWindow.Format(Meet(TimeMode.Fixed, Saturday.AddHours(4))));

	[Fact]
	public void AnOpenStartMeetShowsFromWhenItOpens() =>
		Assert.Equal("Sa, 17.10. · Open Start ab 18:00", MeetTimeWindow.Format(Meet(TimeMode.OpenStart, null)));

	[Fact]
	public void AnOpenEndMeetShowsItsStartAndNoEnd() =>
		Assert.Equal("Sa, 17.10. · ab 18:00, Open End", MeetTimeWindow.Format(Meet(TimeMode.OpenEnd, null)));

	private static Meet Meet(TimeMode mode, DateTime? end) => new()
	{
		Title = "Treff",
		Theme = "Tuning",
		Location = "Parkplatz",
		OrganizerName = "Livio",
		TimeMode = mode,
		StartTime = Saturday,
		EndTime = end,
	};
}