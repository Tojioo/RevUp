using Microsoft.Extensions.Time.Testing;
using RevUp.Services;

namespace RevUp.Tests;

public sealed class FreeSeatsTests : IDisposable
{
	private readonly FakeMeetRepository _repository = new();
	private readonly MeetService _service;

	public FreeSeatsTests() =>
		_service = new MeetService(_repository, new FakeJoinRequestRepository(), new MeetChangeNotifier(), new FakeTimeProvider(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero)));

	public void Dispose() => _service.Dispose();

	[Theory]
	[InlineData(18, "Noch 18 Plätze frei")]
	[InlineData(1, "Noch 1 Platz frei")]
	[InlineData(0, "Ausgebucht")]
	public void TheLabelSaysHowManyPlacesAreLeft(int freeSeats, string expected) => Assert.Equal(expected, SeatLabel.For(freeSeats));

	[Fact]
	public void TheListAndTheDetailsShowTheFreeSeats()
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 3, "Aylin");

		var item = MeetListItem.From(meet);
		var details = MeetDetails.From(meet);

		Assert.Equal(2, item.FreeSeats);
		Assert.Equal("Noch 2 Plätze frei", item.SeatsLabel);
		Assert.Equal(2, details.FreeSeats);
		Assert.Equal("Noch 2 Plätze frei", details.SeatsLabel);
	}

	[Fact]
	public async Task AfterJoiningTheListShowsOneSeatLess()
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 2, "Aylin");
		_repository.Meets.Add(meet);

		await _service.JoinAsync(meet.Id, "Noah");

		var item = Assert.Single(await _service.GetUpcomingMeetsAsync());
		Assert.Equal(0, item.FreeSeats);
		Assert.Equal("Ausgebucht", item.SeatsLabel);
	}
}