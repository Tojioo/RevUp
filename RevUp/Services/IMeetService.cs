using RevUp.Models;

namespace RevUp.Services;

public interface IMeetService
{
	IReadOnlyList<Meet> GetMeets();

	/// <summary>Meets that have not ended yet, soonest first, without their location.</summary>
	IReadOnlyList<MeetListItem> GetUpcomingMeets();

	MeetCreationResult Create(MeetDraft draft);
}