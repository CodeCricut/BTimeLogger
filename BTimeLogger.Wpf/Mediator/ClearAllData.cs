using BTimeLogger.Domain.Services;
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
		private readonly IIntervalRepository _intervalRepository;

		public ClearAllDataHandler(IActivityRepository activityRepository,
			IIntervalRepository intervalRepository)
		{
			_activityRepository = activityRepository;
			_intervalRepository = intervalRepository;
		}

		public async Task<Unit> Handle(ClearAllData request, CancellationToken cancellationToken)
		{
			await _activityRepository.ClearActivities();
			await _intervalRepository.ClearIntervals();

			return Unit.Value;
		}
	}
}
