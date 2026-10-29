namespace RevUp.Services;

/// <summary>Observer: gets told when a meet changed, e.g. a page that shows that meet.</summary>
public interface IMeetObserver
{
	Task OnMeetChangedAsync(Guid meetId);
}