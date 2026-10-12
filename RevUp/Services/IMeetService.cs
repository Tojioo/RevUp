using RevUp.Models;

namespace RevUp.Services;

public interface IMeetService
{
	IReadOnlyList<Meet> GetMeets();

	MeetCreationResult Create(MeetDraft draft);
}