using BTimeLogger.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator
{
	public class ClearAllData : IRequest
	{
	}

	public class ClearAllDataHandler : IRequestHandler<ClearAllData>
	{
		private readonly IActivityRepository _activityRepository;
		private readonly IStatisticsRepository _statisticsRepository;
		private readonly IIntervalRepository _intervalRepository;

		public ClearAllDataHandler(IActivityRepository activityRepository,
			IStatisticsRepository statisticsRepository,
			IIntervalRepository intervalRepository)
		{
			_activityRepository = activityRepository;
			_statisticsRepository = statisticsRepository;
			_intervalRepository = intervalRepository;
		}

		public async Task<Unit> Handle(ClearAllData request, CancellationToken cancellationToken)
		{
			await _activityRepository.ClearActivities();
			await _statisticsRepository.ClearStatistics();
			await _intervalRepository.ClearIntervals();

			return Unit.Value;
		}
	}
}
