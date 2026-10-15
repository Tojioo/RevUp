using Microsoft.Extensions.Time.Testing;
using RevUp.Services;

namespace RevUp.Tests;

public class MeetListTests
{
	private readonly FakeTimeProvider _clock = new(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero));
	private readonly MeetService _service;

	public MeetListTests() => _service = new MeetService(_clock);

	[Fact]
	public void MeetsThatHaveEndedAreNotListed()
	{
		var meet = _service.Create(MeetDrafts.Valid()).Meet!;
		Assert.Contains(_service.GetUpcomingMeets(), item => item.Id == meet.Id);

		_clock.Advance(meet.EndTime!.Value - MeetDrafts.Now + TimeSpan.FromMinutes(1));

		Assert.DoesNotContain(_service.GetUpcomingMeets(), item => item.Id == meet.Id);
	}

	[Fact]
	public void MeetsAreSortedBySoonestStart()
	{
		var starts = _service.GetUpcomingMeets().Select(item => _service.GetMeets().Single(meet => meet.Id == item.Id).StartTime).ToList();

		Assert.NotEmpty(starts);
		Assert.Equal(starts.Order(), starts);
	}

	[Fact]
	public void AnItemShowsTitleThemeTimeWindowAndVisibility()
	{
		var draft = MeetDrafts.Valid();
		draft.IsPublic = false;
		var meet = _service.Create(draft).Meet!;

		var item = _service.GetUpcomingMeets().Single(item => item.Id == meet.Id);

		Assert.Equal(meet.Title, item.Title);
		Assert.Equal(meet.Theme, item.Theme);
		Assert.Equal(MeetTimeWindow.Format(meet), item.TimeWindow);
		Assert.False(item.IsPublic);
		Assert.Equal("Privat", item.VisibilityLabel);
		Assert.Equal("Ort nach Freigabe", item.LocationHint);
		Assert.Equal(0, item.AttendeeCount);
		Assert.Equal(meet.MaxAttendees, item.MaxAttendees);
	}

	[Fact]
	public void TheListCannotRevealTheLocation() =>
		Assert.DoesNotContain(typeof(MeetListItem).GetProperties(), property => property.Name == "Location");
}