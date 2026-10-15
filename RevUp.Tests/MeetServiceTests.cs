using Microsoft.Extensions.Time.Testing;
using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public class MeetServiceTests
{
	private readonly MeetService _service = new(new FakeTimeProvider(new DateTimeOffset(MeetDrafts.Now, TimeSpan.Zero)));

	[Fact]
	public void AValidMeetIsSavedAndAppearsInTheList()
	{
		var countBefore = _service.GetMeets().Count;
		var draft = MeetDrafts.Valid();
		draft.Title = "  Sonntagsausfahrt  ";

		var result = _service.Create(draft);

		Assert.True(result.Succeeded);
		Assert.Empty(result.Errors);
		Assert.Equal(countBefore + 1, _service.GetMeets().Count);
		var meet = Assert.Single(_service.GetMeets(), meet => meet.Id == result.Meet!.Id);
		Assert.Equal("Sonntagsausfahrt", meet.Title);
		Assert.Equal(draft.Theme, meet.Theme);
		Assert.Equal(draft.Location, meet.Location);
		Assert.Equal(draft.OrganizerName, meet.OrganizerName);
		Assert.Equal(TimeMode.Fixed, meet.TimeMode);
		Assert.Equal(draft.StartTime, meet.StartTime);
		Assert.Equal(draft.EndTime, meet.EndTime);
		Assert.Equal(draft.MaxAttendees, meet.MaxAttendees);
		Assert.Equal(draft.MaxVolumeDb, meet.MaxVolumeDb);
		Assert.Equal(0, meet.AttendeeCount);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void TheChosenVisibilityIsSaved(bool isPublic)
	{
		var draft = MeetDrafts.Valid();
		draft.IsPublic = isPublic;

		var result = _service.Create(draft);

		Assert.Equal(isPublic, result.Meet!.IsPublic);
	}

	[Fact]
	public void ANewDraftIsPublicLikeInThePrototype() => Assert.True(new MeetDraft().IsPublic);

	[Fact]
	public void AnInvalidMeetIsNotSaved()
	{
		var countBefore = _service.GetMeets().Count;
		var draft = MeetDrafts.Valid();
		draft.Title = string.Empty;

		var result = _service.Create(draft);

		Assert.False(result.Succeeded);
		Assert.Contains(result.Errors, error => error.Field == nameof(MeetDraft.Title));
		Assert.Equal(countBefore, _service.GetMeets().Count);
	}
}