using RevUp.Models;

namespace RevUp.Services;

public static class JoinPolicies
{
	private static readonly IJoinPolicy Public = new PublicJoinPolicy();
	private static readonly IJoinPolicy Private = new PrivateJoinPolicy();

	public static IJoinPolicy For(Meet meet) => meet.IsPublic ? Public : Private;
}