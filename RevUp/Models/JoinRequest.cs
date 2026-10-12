namespace RevUp.Models;

public class JoinRequest
{
	public Guid Id { get; init; } = Guid.NewGuid();

	public required Guid MeetId { get; init; }

	public required string ParticipantName { get; init; }

	public RequestStatus Status { get; set; } = RequestStatus.Pending;

	public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}