namespace RevUp.Services;

public sealed class PublicJoinPolicy : IJoinPolicy
{
	public string Label => "Öffentlich";

	public bool RequiresApproval => false;

	public string LocationHint => "Ort nach Beitritt";
}