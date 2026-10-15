using RevUp.Models;

namespace RevUp.Services;

public interface IMeetService
{
	/// <summary>Meets that have not ended yet, soonest first, without their location.</summary>
	Task<IReadOnlyList<MeetListItem>> GetUpcomingMeetsAsync(CancellationToken cancellationToken = default);

	Task<MeetCreationResult> CreateAsync(MeetDraft draft, CancellationToken cancellationToken = default);
}