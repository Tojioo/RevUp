using RevUp.Models;

namespace RevUp.Services;

/// <summary>The outcome of a join attempt; <see cref="Location"/> is only set when the participant may see it.</summary>
public sealed record JoinResult(JoinStatus Status, string? Location = null);