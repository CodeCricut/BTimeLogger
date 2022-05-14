using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Controls;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator;

public class GetAllActivityVMsQuery : IRequest<IEnumerable<ActivityViewModel>>
{
	public GetAllActivityVMsQuery()
	{

	}
}

public class GetAllActivityVMsHandler : IRequestHandler<GetAllActivityVMsQuery, IEnumerable<ActivityViewModel>>
{
	private readonly IActivityRepository _activityRepository;
	private readonly IActivityViewModelFactory _activityViewModelFactory;

	public GetAllActivityVMsHandler(IActivityRepository activityRepository,
		IActivityViewModelFactory activityViewModelFactory)
	{
		_activityRepository = activityRepository;
		_activityViewModelFactory = activityViewModelFactory;
	}

	public async Task<IEnumerable<ActivityViewModel>> Handle(GetAllActivityVMsQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<Activity> allActivities = await _activityRepository.GetActivities();

		List<ActivityViewModel> activityViewModels = new();
		foreach (Activity a in allActivities)
			activityViewModels.Add(_activityViewModelFactory.Create(a));

		return activityViewModels;
	}
}
