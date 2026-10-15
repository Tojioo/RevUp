using RevUp.Data;
using RevUp.Models;

namespace RevUp.Services;

public class MeetService : IMeetService
{
	private readonly Lock _lock = new();
	private readonly TimeProvider _clock;
	private readonly List<Meet> _meets;

	public MeetService(TimeProvider clock)
	{
		_clock = clock;
		_meets = [.. MeetSeeder.CreateSampleMeets(clock.GetLocalNow().Date)];
	}

	public IReadOnlyList<Meet> GetMeets()
	{
		lock (_lock)
		{
			return _meets.ToList();
		}
	}

	public IReadOnlyList<MeetListItem> GetUpcomingMeets()
	{
		var now = _clock.GetLocalNow().DateTime;
		return GetMeets()
			.Where(meet => !HasEnded(meet, now))
			.OrderBy(meet => meet.StartTime)
			.Select(MeetListItem.From)
			.ToList();
	}

	public MeetCreationResult Create(MeetDraft draft)
	{
		var errors = MeetDraftValidator.Validate(draft, _clock.GetLocalNow().DateTime);
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

		lock (_lock)
		{
			_meets.Add(meet);
		}

		return new MeetCreationResult(meet, []);
	}

	// Assumption: a meet without an end (Open End) is over once its start day has passed.
	private static bool HasEnded(Meet meet, DateTime now) =>
		meet.EndTime is { } end ? end <= now : meet.StartTime.Date < now.Date;
}