using RevUp.Models;

namespace RevUp.Services;

public sealed class PublicJoinPolicy : IJoinPolicy
{
	public string Label => "Öffentlich";

	public bool RequiresApproval => false;

	public string LocationHint => "Ort nach Beitritt";

	public JoinResult Join(Meet meet, string participantName) => throw new NotImplementedException();
}