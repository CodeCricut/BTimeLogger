using BTimeLogger.Csv;
using BTimeLogger.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator;

public class ClearAllData : IRequest
{
}

public class ClearAllDataHandler : IRequestHandler<ClearAllData>
{
	private readonly IActivityRepository _activityRepository;
	private readonly IIntervalRepository _intervalRepository;
	private readonly ICsvChangeTracker _csvChangeTracker;

	public ClearAllDataHandler(IActivityRepository activityRepository,
		IIntervalRepository intervalRepository,
		ICsvChangeTracker csvChangeTracker)
	{
		_activityRepository = activityRepository;
		_intervalRepository = intervalRepository;
		_csvChangeTracker = csvChangeTracker;
	}

	public async Task<Unit> Handle(ClearAllData request, CancellationToken cancellationToken)
	{
		// TODO Issue #6: Add cancel option to ClearAllData request
		await _activityRepository.Clear();
		await _intervalRepository.Clear();

		await _activityRepository.SaveChanges();
		await _intervalRepository.SaveChanges();

		_csvChangeTracker.MakeChange();

		return Unit.Value;
	}
}
