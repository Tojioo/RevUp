namespace RevUp.Services;

/// <summary>The subject of the observer pattern: <see cref="MeetService"/> reports every change, attached pages refresh themselves.</summary>
public sealed class MeetChangeNotifier : IMeetSubject
{
	private readonly Lock _lock = new();
	private readonly List<IMeetObserver> _observers = [];

	public void Attach(IMeetObserver observer)
	{
		lock (_lock)
		{
			if (!_observers.Contains(observer))
			{
				_observers.Add(observer);
			}
		}
	}

	public void Detach(IMeetObserver observer)
	{
		lock (_lock)
		{
			_observers.Remove(observer);
		}
	}

	public async Task NotifyAsync(Guid meetId)
	{
		IMeetObserver[] observers;
		lock (_lock)
		{
			// Because an observer may detach while being notified, which would change the list during the loop.
			observers = [.. _observers];
		}

		foreach (var observer in observers)
		{
			await observer.OnMeetChangedAsync(meetId);
		}
	}
}