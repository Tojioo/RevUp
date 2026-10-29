namespace RevUp.Services;

public sealed class MeetChangeNotifier : IMeetSubject
{
	public void Attach(IMeetObserver observer) => throw new NotImplementedException();

	public void Detach(IMeetObserver observer) => throw new NotImplementedException();

	public Task NotifyAsync(Guid meetId) => throw new NotImplementedException();
}