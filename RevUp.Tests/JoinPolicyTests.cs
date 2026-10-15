using RevUp.Models;
using RevUp.Services;

namespace RevUp.Tests;

public class JoinPolicyTests
{
	[Fact]
	public void APublicMeetIsJoinedWithoutApproval()
	{
		var policy = JoinPolicies.For(MeetWithVisibility(isPublic: true));

		Assert.False(policy.RequiresApproval);
		Assert.Equal("Öffentlich", policy.Label);
		Assert.Equal("Ort nach Beitritt", policy.LocationHint);
	}

	[Fact]
	public void APrivateMeetNeedsTheOrganizersApproval()
	{
		var policy = JoinPolicies.For(MeetWithVisibility(isPublic: false));

		Assert.True(policy.RequiresApproval);
		Assert.Equal("Privat", policy.Label);
		Assert.Equal("Ort nach Freigabe", policy.LocationHint);
	}

	private static Meet MeetWithVisibility(bool isPublic) => new()
	{
		Title = "Treff",
		Theme = "Tuning",
		Location = "Parkplatz",
		OrganizerName = "Livio",
		IsPublic = isPublic,
	};
}