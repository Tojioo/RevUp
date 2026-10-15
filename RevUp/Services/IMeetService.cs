using RevUp.Models;

namespace RevUp.Services;

public interface IMeetService
{
	/// <summary>Meets that have not ended yet, soonest first, without their location.</summary>
	Task<IReadOnlyList<MeetListItem>> GetUpcomingMeetsAsync(CancellationToken cancellationToken = default);

	/// <summary>The meet without its location, or null if it does not exist.</summary>
	Task<MeetDetails?> GetMeetDetailsAsync(Guid id, CancellationToken cancellationToken = default);

	Task<JoinResult> JoinAsync(Guid id, string participantName, CancellationToken cancellationToken = default);

	Task<MeetCreationResult> CreateAsync(MeetDraft draft, CancellationToken cancellationToken = default);
}