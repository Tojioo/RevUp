using RevUp.Services;

namespace RevUp.Tests;

public class MeetFilterTests
{
	private readonly IReadOnlyList<MeetListItem> _meets =
	[
		Item("Tuning", isPublic: true),
		Item("90s Cars", isPublic: false),
		Item("tuning", isPublic: false),
		Item("Green", isPublic: true),
	];

	[Fact]
	public void WithoutAFilterEveryMeetIsListed() => Assert.Equal(_meets, new MeetFilter().Apply(_meets));

	[Fact]
	public void AThemeKeepsOnlyItsMeetsIgnoringCase()
	{
		var filtered = new MeetFilter(Theme: "Tuning").Apply(_meets);

		Assert.Equal([_meets[0], _meets[2]], filtered);
	}

	[Theory]
	[InlineData(VisibilityFilter.Public, 0, 3)]
	[InlineData(VisibilityFilter.Private, 1, 2)]
	public void TheVisibilityKeepsOnlyPublicOrPrivateMeets(VisibilityFilter visibility, int first, int second)
	{
		var filtered = new MeetFilter(Visibility: visibility).Apply(_meets);

		Assert.Equal([_meets[first], _meets[second]], filtered);
	}

	[Fact]
	public void ThemeAndVisibilityCombine()
	{
		var filtered = new MeetFilter("tuning", VisibilityFilter.Private).Apply(_meets);

		Assert.Equal([_meets[2]], filtered);
	}

	[Fact]
	public void EveryThemeIsOfferedOnceInOrder() =>
		Assert.Equal(["90s Cars", "Green", "Tuning"], MeetFilter.ThemesOf(_meets));

	private static MeetListItem Item(string theme, bool isPublic)
	{
		var meet = MeetFactory.Create(isPublic);
		meet.Theme = theme;
		return MeetListItem.From(meet);
	}
}