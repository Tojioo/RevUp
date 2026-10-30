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
		var name = participantName.Trim();
		if (name.Length is 0 or > _maxNameLength)
		{
			return new JoinResult(JoinStatus.InvalidName);
		}

		JoinResult result;
		var changed = false;
		await _joinLock.WaitAsync(cancellationToken);
		try
		{
			var meet = await meets.GetByIdAsync(id, cancellationToken);
			if (meet is null)
			{
				return new JoinResult(JoinStatus.NotFound);
			}

			(result, changed) = await ApplyJoinAsync(meet, JoinPolicies.For(meet).Join(meet, name), name, cancellationToken);
		}
		finally
		{
			_joinLock.Release();
		}

		// Notified only after the lock is released, because observers may call back into this service.
		if (changed)
		{
			await notifier.NotifyAsync(id);
		}

		return result;
	}

	public async Task<DecisionStatus> DecideAsync(Guid requestId, bool approve, CancellationToken cancellationToken = default)
	{
		DecisionStatus decision;
		Guid meetId;
		await _joinLock.WaitAsync(cancellationToken);
		try
		{
			var request = await requests.GetByIdAsync(requestId, cancellationToken);
			var meet = request is { Status: RequestStatus.Pending } ? await meets.GetByIdAsync(request.MeetId, cancellationToken) : null;
			if (request is null || meet is null)
			{
				return DecisionStatus.NotFound;
			}

			decision = approve ? await ApproveAsync(meet, request, cancellationToken) : DecisionStatus.Rejected;
			if (decision == DecisionStatus.Full)
			{
				return decision;
			}

			request.Status = decision == DecisionStatus.Approved ? RequestStatus.Approved : RequestStatus.Rejected;
			await requests.UpdateAsync(request, cancellationToken);
			meetId = meet.Id;
		}
		finally
		{
			_joinLock.Release();
		}

		await notifier.NotifyAsync(meetId);
		return decision;
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

	public Task<MeetDraft?> GetDraftForEditAsync(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task<MeetSaveResult?> UpdateAsync(Guid id, MeetDraft draft, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public async Task<MeetSaveResult> CreateAsync(MeetDraft draft, CancellationToken cancellationToken = default)
	{
		var errors = MeetDraftValidator.Validate(draft, clock.GetLocalNow().DateTime);
		if (errors.Count > 0)
		{
			return new MeetSaveResult(null, errors);
		}

		var meet = new Meet
		{
			Title = draft.Title.Trim(),
			Theme = draft.Theme.Trim(),
			Location = draft.Location.Trim(),
			OrganizerName = draft.OrganizerName.Trim(),
			TimeMode = draft.TimeMode,
			StartTime = draft.StartTime.GetValueOrDefault(),
			EndTime = draft.EndTime,
			MaxAttendees = draft.MaxAttendees,
			MaxVolumeDb = draft.MaxVolumeDb,
			IsPublic = draft.IsPublic,
		};

		await meets.AddAsync(meet, cancellationToken);
		await notifier.NotifyAsync(meet.Id);
		return new MeetSaveResult(meet, []);
	}

	public void Dispose() => _joinLock.Dispose();

	// Assumption: a meet without an end (Open End) is over once its start day has passed.
	private static bool HasEnded(Meet meet, DateTime now) =>
		meet.EndTime is { } end ? end <= now : meet.StartTime.Date < now.Date;

	private async Task<(JoinResult Result, bool Changed)> ApplyJoinAsync(Meet meet, JoinResult result, string name, CancellationToken cancellationToken)
	{
		if (result.Status == JoinStatus.Joined)
		{
			await meets.UpdateAsync(meet, cancellationToken);
			return (result, true);
		}

		if (result.Status != JoinStatus.RequestPending)
		{
			return (result, false);
		}

		var existing = (await requests.GetForMeetAsync(meet.Id, cancellationToken))
			.FirstOrDefault(request => string.Equals(request.ParticipantName, name, StringComparison.OrdinalIgnoreCase));
		if (existing is not null)
		{
			return (existing.Status == RequestStatus.Rejected ? new JoinResult(JoinStatus.Rejected) : result, false);
		}

		var request = new JoinRequest
		{
			MeetId = meet.Id,
			ParticipantName = name,
			CreatedAt = clock.GetLocalNow().DateTime,
		};
		await requests.AddAsync(request, cancellationToken);
		return (result, true);
	}

	private async Task<DecisionStatus> ApproveAsync(Meet meet, JoinRequest request, CancellationToken cancellationToken)
	{
		if (!meet.AttendeeNames.Contains(request.ParticipantName, StringComparer.OrdinalIgnoreCase))
		{
			if (meet.IsFull)
			{
				return DecisionStatus.Full;
			}

			meet.AttendeeNames.Add(request.ParticipantName);
			await meets.UpdateAsync(meet, cancellationToken);
		}

		return DecisionStatus.Approved;
	}
}