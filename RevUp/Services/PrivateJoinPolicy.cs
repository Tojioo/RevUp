using RevUp.Models;

namespace RevUp.Services;

public sealed class PrivateJoinPolicy : IJoinPolicy
{
	public string Label => "Privat";

	public bool RequiresApproval => true;

	public string LocationHint => "Ort nach Freigabe";

	public JoinResult Join(Meet meet, string participantName) =>
		meet.AttendeeNames.Contains(participantName.Trim(), StringComparer.OrdinalIgnoreCase)
			? new JoinResult(JoinStatus.AlreadyJoined, meet.Location)
			: new JoinResult(JoinStatus.RequestPending);
}