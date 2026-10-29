namespace RevUp.Services;

/// <summary>An open request as the organizer sees it.</summary>
public sealed record JoinRequestItem(Guid Id, string ParticipantName, DateTime CreatedAt);