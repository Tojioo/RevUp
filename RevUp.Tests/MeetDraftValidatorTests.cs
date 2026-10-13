using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public class MeetDraftValidatorTests
{
	public static TheoryData<string, Action<MeetDraft>, string> InvalidDrafts => new()
	{
		{ nameof(MeetDraft.Title), draft => draft.Title = string.Empty, "Pflichtfeld." },
		{ nameof(MeetDraft.Theme), draft => draft.Theme = "   ", "Pflichtfeld." },
		{ nameof(MeetDraft.Location), draft => draft.Location = string.Empty, "Pflichtfeld." },
		{ nameof(MeetDraft.OrganizerName), draft => draft.OrganizerName = string.Empty, "Pflichtfeld." },
		{ nameof(MeetDraft.Title), draft => draft.Title = new string('x', 81), "Höchstens 80 Zeichen." },
		{ nameof(MeetDraft.Theme), draft => draft.Theme = new string('x', 41), "Höchstens 40 Zeichen." },
		{ nameof(MeetDraft.Location), draft => draft.Location = new string('x', 121), "Höchstens 120 Zeichen." },
		{ nameof(MeetDraft.OrganizerName), draft => draft.OrganizerName = new string('x', 41), "Höchstens 40 Zeichen." },
		{ nameof(MeetDraft.StartTime), draft => draft.StartTime = null, "Pflichtfeld." },
		{ nameof(MeetDraft.StartTime), draft => draft.StartTime = MeetDrafts.Now.AddMinutes(-1), "Der Start muss in der Zukunft liegen." },
		{ nameof(MeetDraft.EndTime), draft => draft.EndTime = null, "Pflichtfeld." },
		{ nameof(MeetDraft.EndTime), draft => draft.EndTime = draft.StartTime, "Das Ende muss nach dem Start liegen." },
		{ nameof(MeetDraft.MaxAttendees), draft => draft.MaxAttendees = 1, "Zwischen 2 und 500." },
		{ nameof(MeetDraft.MaxAttendees), draft => draft.MaxAttendees = 501, "Zwischen 2 und 500." },
		{ nameof(MeetDraft.MaxVolumeDb), draft => draft.MaxVolumeDb = 49, "Zwischen 50 und 130 dB." },
		{ nameof(MeetDraft.MaxVolumeDb), draft => draft.MaxVolumeDb = 131, "Zwischen 50 und 130 dB." },
	};

	[Fact]
	public void AValidDraftHasNoErrors() => Assert.Empty(MeetDraftValidator.Validate(MeetDrafts.Valid(), MeetDrafts.Now));

	[Theory]
	[MemberData(nameof(InvalidDrafts))]
	public void AnInvalidFieldIsReportedWithItsMessage(string field, Action<MeetDraft> breakField, string message)
	{
		var draft = MeetDrafts.Valid();
		breakField(draft);

		var errors = MeetDraftValidator.Validate(draft, MeetDrafts.Now);

		Assert.Equal([new ValidationError(field, message)], errors);
	}
}