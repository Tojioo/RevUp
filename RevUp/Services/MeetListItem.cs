using RevUp.Models;

namespace RevUp.Services;

/// <summary>A meet as the public list shows it. Deliberately has no location, so the list can never reveal it.</summary>
public sealed record MeetListItem(
	Guid Id,
	string Title,
	string Theme,
	string TimeWindow,
	bool IsPublic,
	string VisibilityLabel,
	string LocationHint,
	int AttendeeCount,
	int MaxAttendees)
{
	public int FreeSeats => Math.Max(0, MaxAttendees - AttendeeCount);

	public string SeatsLabel => SeatLabel.For(FreeSeats);

	public static MeetListItem From(Meet meet)
	{
		var policy = JoinPolicies.For(meet);
		return new MeetListItem(
			meet.Id,
			meet.Title,
			meet.Theme,
			MeetTimeWindow.Format(meet),
			meet.IsPublic,
			policy.Label,
			policy.LocationHint,
			meet.AttendeeCount,
			meet.MaxAttendees);
	}
}