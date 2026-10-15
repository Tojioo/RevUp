using RevUp.Models;

namespace RevUp.Services;

public sealed class PublicJoinPolicy : IJoinPolicy
{
	public string Label => "Öffentlich";

	public bool RequiresApproval => false;

	public string LocationHint => "Ort nach Beitritt";

	public JoinResult Join(Meet meet, string participantName)
	{
		var name = participantName.Trim();
		if (meet.AttendeeNames.Contains(name, StringComparer.OrdinalIgnoreCase))
		{
			return new JoinResult(JoinStatus.AlreadyJoined, meet.Location);
		}

		if (meet.IsFull)
		{
			return new JoinResult(JoinStatus.Full);
		}

		meet.AttendeeNames.Add(name);
		return new JoinResult(JoinStatus.Joined, meet.Location);
	}
}