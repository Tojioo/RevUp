using RevUp.Models;

namespace RevUp.Services;

public sealed record MeetCreationResult(Meet? Meet, IReadOnlyList<ValidationError> Errors)
{
	public bool Succeeded => Meet is not null;
}