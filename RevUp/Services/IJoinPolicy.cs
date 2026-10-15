using RevUp.Models;

namespace RevUp.Services;

/// <summary>How a participant gets into a meet and when the location becomes visible; one implementation per visibility.</summary>
public interface IJoinPolicy
{
	string Label { get; }

	bool RequiresApproval { get; }

	string LocationHint { get; }

	/// <summary>Lets a participant into the meet as far as this policy allows; changes the meet's attendees.</summary>
	JoinResult Join(Meet meet, string participantName);
}