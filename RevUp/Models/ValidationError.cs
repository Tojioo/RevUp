namespace RevUp.Models;

/// <summary>A rule an input violates; <see cref="Field"/> is the property name, the message is shown to the user.</summary>
public sealed record ValidationError(string Field, string Message);