using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public sealed class MeetListTests : IDisposable
{
	private readonly FakeTimeProvider _clock = new(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero));
	private readonly FakeMeetRepository _repository = new();
	private readonly MeetService _service;

	public MeetListTests() => _service = new MeetService(_repository, _clock);

	public void Dispose() => _service.Dispose();

	[Fact]
	public async Task MeetsThatHaveEndedAreNotListed()
	{
		var meet = (await _service.CreateAsync(MeetDrafts.Valid())).Meet!;
		Assert.Contains(await _service.GetUpcomingMeetsAsync(), item => item.Id == meet.Id);

		_clock.Advance(meet.EndTime!.Value - MeetDrafts.Now + TimeSpan.FromMinutes(1));

		Assert.DoesNotContain(await _service.GetUpcomingMeetsAsync(), item => item.Id == meet.Id);
	}

	[Fact]
	public async Task MeetsAreSortedBySoonestStart()
	{
		int[] daysAheadOutOfOrder = [3, 1, 2];
		foreach (var daysAhead in daysAheadOutOfOrder)
		{
			var draft = MeetDrafts.Valid();
			draft.Title = $"In {daysAhead} Tagen";
			draft.StartTime = MeetDrafts.Now.AddDays(daysAhead);
			draft.EndTime = draft.StartTime.Value.AddHours(3);
			await _service.CreateAsync(draft);
		}

		var titles = (await _service.GetUpcomingMeetsAsync()).Select(item => item.Title);

		Assert.Equal(["In 1 Tagen", "In 2 Tagen", "In 3 Tagen"], titles);
	}

	[Fact]
	public async Task AnItemShowsTitleThemeTimeWindowAndVisibility()
	{
		var draft = MeetDrafts.Valid();
		draft.IsPublic = false;
		var meet = (await _service.CreateAsync(draft)).Meet!;

		var item = Assert.Single(await _service.GetUpcomingMeetsAsync());

		Assert.Equal(meet.Id, item.Id);
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
		Assert.DoesNotContain(typeof(MeetListItem).GetProperties(), property => property.Name == nameof(Meet.Location));
}