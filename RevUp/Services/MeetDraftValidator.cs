using RevUp.Models;

namespace RevUp.Services;

/// <summary>Checks a new meet on the server, so the rules hold no matter what the form sends.</summary>
public static class MeetDraftValidator
{
	private const string _required = "Pflichtfeld.";

	public static IReadOnlyList<ValidationError> Validate(MeetDraft draft, DateTime now)
	{
		var errors = new List<ValidationError>();
		CheckText(errors, nameof(MeetDraft.Title), draft.Title, 80);
		CheckText(errors, nameof(MeetDraft.Theme), draft.Theme, 40);
		CheckText(errors, nameof(MeetDraft.Location), draft.Location, 120);
		CheckText(errors, nameof(MeetDraft.OrganizerName), draft.OrganizerName, 40);
		CheckTimes(errors, draft, now);
		CheckRange(errors, nameof(MeetDraft.MaxAttendees), draft.MaxAttendees, 2, 500, "Zwischen 2 und 500.");
		CheckRange(errors, nameof(MeetDraft.MaxVolumeDb), draft.MaxVolumeDb, 50, 130, "Zwischen 50 und 130 dB.");
		return errors;
	}

	private static void CheckText(List<ValidationError> errors, string field, string value, int maxLength)
	{
		var length = value.Trim().Length;
		if (length == 0)
		{
			errors.Add(new ValidationError(field, _required));
		}
		else if (length > maxLength)
		{
			errors.Add(new ValidationError(field, $"Höchstens {maxLength} Zeichen."));
		}
	}

	private static void CheckTimes(List<ValidationError> errors, MeetDraft draft, DateTime now)
	{
		if (draft.StartTime is not { } start)
		{
			errors.Add(new ValidationError(nameof(MeetDraft.StartTime), _required));
		}
		else if (start <= now)
		{
			errors.Add(new ValidationError(nameof(MeetDraft.StartTime), "Der Start muss in der Zukunft liegen."));
		}

		if (draft.EndTime is not { } end)
		{
			errors.Add(new ValidationError(nameof(MeetDraft.EndTime), _required));
		}
		else if (end <= draft.StartTime)
		{
			errors.Add(new ValidationError(nameof(MeetDraft.EndTime), "Das Ende muss nach dem Start liegen."));
		}
	}

	private static void CheckRange(List<ValidationError> errors, string field, int value, int min, int max, string message)
	{
		if (value < min || value > max)
		{
			errors.Add(new ValidationError(field, message));
		}
	}
}