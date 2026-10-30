namespace RevUp.Models;

/// <summary>The organizer's input for a new meet, before it is validated.</summary>
public class MeetDraft
{
	public string Title { get; set; } = string.Empty;

	public string Theme { get; set; } = string.Empty;

	public string Location { get; set; } = string.Empty;

	public string OrganizerName { get; set; } = string.Empty;

	public TimeMode TimeMode { get; set; } = TimeMode.Fixed;

	public DateTime? StartTime { get; set; }

	public DateTime? EndTime { get; set; }

	public int MaxAttendees { get; set; } = 20;

	public int MaxVolumeDb { get; set; } = 85;

	public bool IsPublic { get; set; } = true;
}