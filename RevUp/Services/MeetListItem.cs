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
	int MaxAttendees);