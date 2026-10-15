using RevUp.Models;
using System.Globalization;

namespace RevUp.Services;

public static class MeetTimeWindow
{
	private static readonly CultureInfo German = CultureInfo.GetCultureInfo("de-CH");

	/// <summary>The day and time span of a meet in German, e.g. "Sa, 17.10. · 18:00–22:00".</summary>
	public static string Format(Meet meet)
	{
		var day = meet.StartTime.ToString("ddd, dd.MM.", German);
		var start = meet.StartTime.ToString("HH:mm", German);
		var time = meet.TimeMode switch
		{
			TimeMode.OpenStart => $"Open Start ab {start}",
			TimeMode.OpenEnd => $"ab {start}, Open End",
			_ => meet.EndTime is { } end ? $"{start}–{end.ToString("HH:mm", German)}" : start,
		};
		return $"{day} · {time}";
	}
}