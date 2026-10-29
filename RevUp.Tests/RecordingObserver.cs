using RevUp.Services;

namespace RevUp.Tests;

/// <summary>Remembers which meets it was told about.</summary>
internal sealed class RecordingObserver : IMeetObserver
{
	public List<Guid> ChangedMeets { get; } = [];

	public Task OnMeetChangedAsync(Guid meetId)
	{
		ChangedMeets.Add(meetId);
		return Task.CompletedTask;
	}
}