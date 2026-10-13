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
		var today = clock.GetLocalNow().Date;
		var nextSaturday = today.AddDays(((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7);
		var nextSunday = nextSaturday.AddDays(1);

		_meets =
		[
			new Meet
			{
				Title = "Sonntagsausfahrt Tuning-Treff",
				Theme = "Tuning",
				Location = "Parkplatz Industriestrasse 12, Zürich",
				OrganizerName = "Livio",
				IsPublic = true,
				TimeMode = TimeMode.Fixed,
				StartTime = nextSaturday.AddHours(18),
				EndTime = nextSaturday.AddHours(22),
				MaxAttendees = 30,
				MaxVolumeDb = 90,
				AttendeeNames = SampleNames(12),
			},
			new Meet
			{
				Title = "90er Treffen Parkhaus Nord",
				Theme = "90s Cars",
				Location = "Parkhaus Nord, Ebene 3",
				OrganizerName = "Livio",
				IsPublic = false,
				TimeMode = TimeMode.OpenStart,
				StartTime = nextSaturday.AddHours(18),
				MaxAttendees = 25,
				MaxVolumeDb = 85,
				AttendeeNames = SampleNames(18),
			},
			new Meet
			{
				Title = "EV & Hybrid Stammtisch",
				Theme = "Green",
				Location = "Café Ampere, Bahnhofstrasse 4",
				OrganizerName = "Aylin",
				IsPublic = true,
				TimeMode = TimeMode.Fixed,
				StartTime = nextSunday.AddHours(14),
				EndTime = nextSunday.AddHours(17),
				MaxAttendees = 20,
				MaxVolumeDb = 70,
				AttendeeNames = SampleNames(6),
			},
		];
	}

	public IReadOnlyList<Meet> GetMeets()
	{
		lock (_lock)
		{
			return _meets.ToList();
		}
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
		};

		lock (_lock)
		{
			_meets.Add(meet);
		}

		return new MeetCreationResult(meet, []);
	}

	private static List<string> SampleNames(int count)
	{
		string[] pool = ["Aylin", "Livio", "Marco", "Sina", "Noah", "Elif", "Timo", "Léa", "Kevin", "Zoe"];
		return Enumerable.Range(0, count).Select(i => pool[i % pool.Length]).ToList();
	}
}