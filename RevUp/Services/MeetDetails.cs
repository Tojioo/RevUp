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
	public static MeetDetails From(Meet meet) => throw new NotImplementedException();
}