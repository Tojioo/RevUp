using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public sealed class MeetTimeModeTests : IDisposable
{
	private readonly FakeMeetRepository _repository = new();
	private readonly MeetService _service;

	public MeetTimeModeTests() =>
		_service = new MeetService(_repository, new FakeJoinRequestRepository(), new MeetChangeNotifier(), new FakeTimeProvider(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero)));

	public void Dispose() => _service.Dispose();

	[Fact]
	public void AnOpenEndMeetNeedsNoEnd() =>
		Assert.Empty(MeetDraftValidator.Validate(Draft(TimeMode.OpenEnd, end: null), MeetDrafts.Now));

	[Fact]
	public void AnOpenEndMeetCannotHaveAnEnd() =>
		Assert.Equal(
			[new ValidationError(nameof(MeetDraft.EndTime), "Bei Open End gibt es kein festes Ende.")],
			MeetDraftValidator.Validate(Draft(TimeMode.OpenEnd, end: MeetDrafts.Now.AddDays(1).AddHours(4)), MeetDrafts.Now));

	[Fact]
	public void AnOpenStartMeetMayLeaveTheEndOpen() =>
		Assert.Empty(MeetDraftValidator.Validate(Draft(TimeMode.OpenStart, end: null), MeetDrafts.Now));

	[Fact]
	public void AnOpenStartMeetWithAnEndStillNeedsItAfterTheStart() =>
		Assert.Equal(
			[new ValidationError(nameof(MeetDraft.EndTime), "Das Ende muss nach dem Start liegen.")],
			MeetDraftValidator.Validate(Draft(TimeMode.OpenStart, end: MeetDrafts.Now.AddDays(1).AddHours(-1)), MeetDrafts.Now));

	[Fact]
	public void AFixedMeetStillNeedsAnEnd() =>
		Assert.Equal(
			[new ValidationError(nameof(MeetDraft.EndTime), "Pflichtfeld.")],
			MeetDraftValidator.Validate(Draft(TimeMode.Fixed, end: null), MeetDrafts.Now));

	[Theory]
	[InlineData(TimeMode.OpenStart, "Open Start ab")]
	[InlineData(TimeMode.OpenEnd, "Open End")]
	public async Task TheChosenTimeModeIsSavedAndShownOnTheDetailPage(TimeMode mode, string expectedText)
	{
		var result = await _service.CreateAsync(Draft(mode, end: null));

		var meet = Assert.Single(_repository.Meets);
		Assert.Equal(mode, meet.TimeMode);
		Assert.Null(meet.EndTime);
		Assert.Contains(expectedText, (await _service.GetMeetDetailsAsync(result.Meet!.Id))!.TimeWindow);
	}

	[Fact]
	public void AnOpenStartMeetWithAnEndShowsBoth()
	{
		var meet = MeetFactory.Create(isPublic: true);
		meet.TimeMode = TimeMode.OpenStart;
		meet.StartTime = new DateTime(2026, 10, 17, 18, 0, 0);
		meet.EndTime = new DateTime(2026, 10, 17, 23, 0, 0);

		Assert.Equal("Sa, 17.10. · Open Start ab 18:00, Ende 23:00", MeetTimeWindow.Format(meet));
	}

	private static MeetDraft Draft(TimeMode mode, DateTime? end)
	{
		var draft = MeetDrafts.Valid();
		draft.TimeMode = mode;
		draft.EndTime = end;
		return draft;
	}
}