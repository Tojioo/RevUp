using RevUp.Models;

namespace RevUp.Services;

/// <summary>A meet as its detail page shows it before joining. Deliberately has no location.</summary>
public sealed record MeetDetails(
	Guid Id,
	string Title,
	string Theme,
	string TimeWindow,
	string OrganizerName,
	int MaxVolumeDb,
	bool IsPublic,
	string VisibilityLabel,
	string LocationHint,
	int AttendeeCount,
	int MaxAttendees)
{
	public int FreeSeats => Math.Max(0, MaxAttendees - AttendeeCount);

	public string SeatsLabel => SeatLabel.For(FreeSeats);

	public static MeetDetails From(Meet meet)
	{
		var policy = JoinPolicies.For(meet);
		return new MeetDetails(
			meet.Id,
			meet.Title,
			meet.Theme,
			MeetTimeWindow.Format(meet),
			meet.OrganizerName,
			meet.MaxVolumeDb,
			meet.IsPublic,
			policy.Label,
			policy.LocationHint,
			meet.AttendeeCount,
			meet.MaxAttendees);
	}
}