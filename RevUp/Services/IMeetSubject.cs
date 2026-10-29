namespace RevUp.Services;

/// <summary>Subject: observers attach to hear about changes to meets and detach when they no longer care.</summary>
public interface IMeetSubject
{
	void Attach(IMeetObserver observer);

	void Detach(IMeetObserver observer);
}