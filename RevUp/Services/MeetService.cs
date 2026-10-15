using RevUp.Data;
using RevUp.Models;

namespace RevUp.Services;

public class MeetService(IMeetRepository meets, TimeProvider clock) : IMeetService
{
	public async Task<IReadOnlyList<MeetListItem>> GetUpcomingMeetsAsync(CancellationToken cancellationToken = default)
	{
		var now = clock.GetLocalNow().DateTime;
		var all = await meets.GetAllAsync(cancellationToken);
		return all
			.Where(meet => !HasEnded(meet, now))
			.OrderBy(meet => meet.StartTime)
			.Select(MeetListItem.From)
			.ToList();
	}

	public async Task<MeetCreationResult> CreateAsync(MeetDraft draft, CancellationToken cancellationToken = default)
	{
		var errors = MeetDraftValidator.Validate(draft, clock.GetLocalNow().DateTime);
		if (errors.Count > 0)
		{
			return new MeetCreationResult(null, errors);
		}

		var meet = new Meet
		{
			Title = draft.Title.Trim(),
			Theme = draft.Theme.Trim(),
			Location = draft.Location.Trim(),
			OrganizerName = draft.OrganizerName.Trim(),
			TimeMode = TimeMode.Fixed,
			StartTime = draft.StartTime.GetValueOrDefault(),
			EndTime = draft.EndTime,
			MaxAttendees = draft.MaxAttendees,
			MaxVolumeDb = draft.MaxVolumeDb,
			IsPublic = draft.IsPublic,
		};

		await meets.AddAsync(meet, cancellationToken);
		return new MeetCreationResult(meet, []);
	}

	// Assumption: a meet without an end (Open End) is over once its start day has passed.
	private static bool HasEnded(Meet meet, DateTime now) =>
		meet.EndTime is { } end ? end <= now : meet.StartTime.Date < now.Date;
}