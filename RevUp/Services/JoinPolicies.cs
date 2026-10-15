using RevUp.Models;

namespace RevUp.Services;

public static class JoinPolicies
{
	private static readonly IJoinPolicy _public = new PublicJoinPolicy();
	private static readonly IJoinPolicy _private = new PrivateJoinPolicy();

	public static IJoinPolicy For(Meet meet) => meet.IsPublic ? _public : _private;
}