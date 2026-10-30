namespace RevUp.Services;

/// <summary>The filter on the discover page; a theme of null means every theme.</summary>
public sealed record MeetFilter(string? Theme = null, VisibilityFilter Visibility = VisibilityFilter.All)
{
	/// <summary>The themes to offer as filter chips, each once (ignoring case) and sorted.</summary>
	public static IReadOnlyList<string> ThemesOf(IEnumerable<MeetListItem> meets) =>
		meets
			.Select(meet => meet.Theme)
			.Distinct(StringComparer.OrdinalIgnoreCase)
			.Order(StringComparer.OrdinalIgnoreCase)
			.ToList();

	public IReadOnlyList<MeetListItem> Apply(IEnumerable<MeetListItem> meets) =>
		meets
			.Where(meet => Theme is null || string.Equals(meet.Theme, Theme, StringComparison.OrdinalIgnoreCase))
			.Where(meet => Visibility switch
			{
				VisibilityFilter.Public => meet.IsPublic,
				VisibilityFilter.Private => !meet.IsPublic,
				_ => true,
			})
			.ToList();
}