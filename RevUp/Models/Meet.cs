namespace RevUp.Models;

public class Meet
{
	public Guid Id { get; init; } = Guid.NewGuid();

	public required string Title { get; set; }

	public required string Theme { get; set; }

	public required string Location { get; set; }

	public required string OrganizerName { get; set; }

	public bool IsPublic { get; set; }

	public TimeMode TimeMode { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime? EndTime { get; set; }

	public int MaxAttendees { get; set; }

	public int MaxVolumeDb { get; set; }

	public List<string> AttendeeNames { get; init; } = [];

	public int AttendeeCount => AttendeeNames.Count;

	public bool IsFull => AttendeeCount >= MaxAttendees;
}