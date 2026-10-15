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

	[Fact]
	public void JoiningAPublicMeetAddsTheNameAndRevealsTheLocation()
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 20, "Aylin");

		var result = JoinPolicies.For(meet).Join(meet, "  Noah  ");

		Assert.Equal(new JoinResult(JoinStatus.Joined, meet.Location), result);
		Assert.Equal(["Aylin", "Noah"], meet.AttendeeNames);
	}

	[Fact]
	public void AFullPublicMeetCannotBeJoined()
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 2, "Aylin", "Livio");

		var result = JoinPolicies.For(meet).Join(meet, "Noah");

		Assert.Equal(new JoinResult(JoinStatus.Full), result);
		Assert.Equal(2, meet.AttendeeCount);
	}

	[Fact]
	public void JoiningAgainWithTheSameNameDoesNotCountTwice()
	{
		var meet = MeetFactory.Create(isPublic: true, maxAttendees: 1, "Noah");

		var result = JoinPolicies.For(meet).Join(meet, "noah");

		Assert.Equal(new JoinResult(JoinStatus.AlreadyJoined, meet.Location), result);
		Assert.Equal(1, meet.AttendeeCount);
	}

	[Fact]
	public void JoiningAPrivateMeetNeverRevealsTheLocation()
	{
		var meet = MeetFactory.Create(isPublic: false);

		var result = JoinPolicies.For(meet).Join(meet, "Noah");

		Assert.Equal(new JoinResult(JoinStatus.RequestPending), result);
		Assert.Empty(meet.AttendeeNames);
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