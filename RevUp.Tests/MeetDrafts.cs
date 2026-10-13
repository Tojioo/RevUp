using RevUp.Models;

namespace RevUp.Tests;

internal static class MeetDrafts
{
	public static readonly DateTime Now = new(2026, 10, 12, 12, 0, 0);

	public static MeetDraft Valid() => new()
	{
		Title = "Sonntagsausfahrt Tuning-Treff",
		Theme = "Tuning",
		Location = "Parkplatz Industriestrasse 12, Zürich",
		OrganizerName = "Livio",
		StartTime = Now.AddDays(1),
		EndTime = Now.AddDays(1).AddHours(3),
		MaxAttendees = 30,
		MaxVolumeDb = 90,
	};
}