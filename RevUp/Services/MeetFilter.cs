namespace RevUp.Services;

/// <summary>The filter on the discover page; a theme of null means every theme.</summary>
public sealed record MeetFilter(string? Theme = null, VisibilityFilter Visibility = VisibilityFilter.All)
{
	/// <summary>The themes to offer as filter chips, each once (ignoring case) and sorted.</summary>
	public static IReadOnlyList<string> ThemesOf(IEnumerable<MeetListItem> meets) => throw new NotImplementedException();

	public IReadOnlyList<MeetListItem> Apply(IEnumerable<MeetListItem> meets) => throw new NotImplementedException();
}