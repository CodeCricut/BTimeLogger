using BTimeLogger.Csv;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Controls;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator;

public class CreateNewActivity : IRequest<ActivityCode>
{
	public CreateNewActivity(Activity newActivity)
	{
		NewActivity = newActivity;
	}

	public Activity NewActivity { get; }
}

public class CreateNewActivityHandler : IRequestHandler<CreateNewActivity, ActivityCode>
{
	private readonly IActivityRepository _activityRepository;
	private readonly IEventAggregator _ea;
	private readonly ICsvChangeTracker _csvChangeTracker;

	public CreateNewActivityHandler(IActivityRepository activityRepository,
		IEventAggregator ea,
		ICsvChangeTracker csvChangeTracker)
	{
		_activityRepository = activityRepository;
		_ea = ea;
		_csvChangeTracker = csvChangeTracker;
	}

	public async Task<ActivityCode> Handle(CreateNewActivity request, CancellationToken cancellationToken)
	{
		ActivityCode code = request.NewActivity.Code;
		await _activityRepository.AddActivity(request.NewActivity);
		await _activityRepository.SaveChanges();

		_csvChangeTracker.MakeChange();

		_ea.SendMessage(new ReportSourceChanged());

		return code;
	}
}