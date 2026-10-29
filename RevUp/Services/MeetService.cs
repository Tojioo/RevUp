using RevUp.Data;
using RevUp.Models;

namespace RevUp.Services;

public sealed class MeetService(IMeetRepository meets, IJoinRequestRepository requests, MeetChangeNotifier notifier, TimeProvider clock) : IMeetService, IDisposable
{
	private const int _maxNameLength = 40;

	// Because two people could otherwise take the last seat of a meet at the same moment.
	private readonly SemaphoreSlim _joinLock = new(1, 1);

	public async Task<IReadOnlyList<MeetListItem>> GetUpcomingMeetsAsync(CancellationToken cancellationToken = default)
	{
		var now = clock.GetLocalNow().DateTime;
		var all = await meets.GetAllAsync(cancellationToken);
		return all
			.Where(meet => !HasEnded(meet, now))
			.OrderBy(meet => meet.StartTime)
			.Select(MeetListItem.From)
			.ToList();
	}

	public async Task<MeetDetails?> GetMeetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var meet = await meets.GetByIdAsync(id, cancellationToken);
		return meet is null ? null : MeetDetails.From(meet);
	}

	public async Task<JoinResult> JoinAsync(Guid id, string participantName, CancellationToken cancellationToken = default)
	{
		if (participantName.Trim().Length is 0 or > _maxNameLength)
		{
			return new JoinResult(JoinStatus.InvalidName);
		}

		await _joinLock.WaitAsync(cancellationToken);
		try
		{
			var meet = await meets.GetByIdAsync(id, cancellationToken);
			if (meet is null)
			{
				return new JoinResult(JoinStatus.NotFound);
			}

			var result = JoinPolicies.For(meet).Join(meet, participantName);
			switch (result.Status)
			{
				case JoinStatus.Joined:
					await meets.UpdateAsync(meet, cancellationToken);
					break;
				case JoinStatus.RequestPending:
					await AddRequestOnceAsync(meet.Id, participantName.Trim(), cancellationToken);
					break;
			}

			return result;
		}
		finally
		{
			_joinLock.Release();
		}
	}

	public async Task<IReadOnlyList<JoinRequestItem>> GetPendingRequestsAsync(Guid meetId, CancellationToken cancellationToken = default)
	{
		var all = await requests.GetForMeetAsync(meetId, cancellationToken);
		return all
			.Where(request => request.Status == RequestStatus.Pending)
			.OrderBy(request => request.CreatedAt)
			.Select(request => new JoinRequestItem(request.Id, request.ParticipantName, request.CreatedAt))
			.ToList();
	}

	public Task<DecisionStatus> DecideAsync(Guid requestId, bool approve, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public async Task<MeetCreationResult> CreateAsync(MeetDraft draft, CancellationToken cancellationToken = default)
	{
		var errors = MeetDraftValidator.Validate(draft, clock.GetLocalNow().DateTime);
		if (errors.Count > 0)
		{
			return new MeetCreationResult(null, errors);
		}

		var meet = new Meet
		{
			Title = draft.Title.Trim(),
			Theme = draft.Theme.Trim(),
			Location = draft.Location.Trim(),
			OrganizerName = draft.OrganizerName.Trim(),
			TimeMode = TimeMode.Fixed,
			StartTime = draft.StartTime.GetValueOrDefault(),
			EndTime = draft.EndTime,
			MaxAttendees = draft.MaxAttendees,
			MaxVolumeDb = draft.MaxVolumeDb,
			IsPublic = draft.IsPublic,
		};

		await meets.AddAsync(meet, cancellationToken);
		return new MeetCreationResult(meet, []);
	}

	public void Dispose() => _joinLock.Dispose();

	// Assumption: a meet without an end (Open End) is over once its start day has passed.
	private static bool HasEnded(Meet meet, DateTime now) =>
		meet.EndTime is { } end ? end <= now : meet.StartTime.Date < now.Date;

	private async Task AddRequestOnceAsync(Guid meetId, string participantName, CancellationToken cancellationToken)
	{
		var existing = await requests.GetForMeetAsync(meetId, cancellationToken);
		if (existing.Any(request => string.Equals(request.ParticipantName, participantName, StringComparison.OrdinalIgnoreCase)))
		{
			return;
		}

		var request = new JoinRequest
		{
			MeetId = meetId,
			ParticipantName = participantName,
			CreatedAt = clock.GetLocalNow().DateTime,
		};
		await requests.AddAsync(request, cancellationToken);
	}
}