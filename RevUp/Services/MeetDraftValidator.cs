using RevUp.Models;

namespace RevUp.Services;

/// <summary>Checks a new meet on the server, so the rules hold no matter what the form sends.</summary>
public static class MeetDraftValidator
{
	public static IReadOnlyList<ValidationError> Validate(MeetDraft draft, DateTime now) => throw new NotImplementedException();
}