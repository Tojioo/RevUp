using RevUp.Models;

namespace RevUp.Tests;

internal static class MeetFactory
{
	public static Meet Create(bool isPublic, int maxAttendees = 20, params string[] attendees) => new()
	{
		Title = "Treff",
		Theme = "Tuning",
		Location = "Parkplatz Industriestrasse 12, Zürich",
		OrganizerName = "Livio",
		IsPublic = isPublic,
		StartTime = MeetDrafts.Now.AddDays(1),
		EndTime = MeetDrafts.Now.AddDays(1).AddHours(3),
		MaxAttendees = maxAttendees,
		AttendeeNames = [.. attendees],
	};
}