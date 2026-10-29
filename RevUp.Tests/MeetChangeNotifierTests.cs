using RevUp.Services;

namespace RevUp.Tests;

public class MeetChangeNotifierTests
{
	private readonly MeetChangeNotifier _notifier = new();
	private readonly RecordingObserver _observer = new();

	[Fact]
	public async Task AnAttachedObserverIsToldWhichMeetChanged()
	{
		var meetId = Guid.NewGuid();
		_notifier.Attach(_observer);

		await _notifier.NotifyAsync(meetId);

		Assert.Equal([meetId], _observer.ChangedMeets);
	}

	[Fact]
	public async Task ADetachedObserverIsNoLongerTold()
	{
		_notifier.Attach(_observer);
		_notifier.Detach(_observer);

		await _notifier.NotifyAsync(Guid.NewGuid());

		Assert.Empty(_observer.ChangedMeets);
	}

	[Fact]
	public async Task AttachingTwiceStillTellsTheObserverOnce()
	{
		_notifier.Attach(_observer);
		_notifier.Attach(_observer);

		await _notifier.NotifyAsync(Guid.NewGuid());

		Assert.Single(_observer.ChangedMeets);
	}

	[Fact]
	public async Task EveryAttachedObserverIsTold()
	{
		var other = new RecordingObserver();
		_notifier.Attach(_observer);
		_notifier.Attach(other);

		await _notifier.NotifyAsync(Guid.NewGuid());

		Assert.Single(_observer.ChangedMeets);
		Assert.Single(other.ChangedMeets);
	}
}