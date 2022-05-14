using MediatR;

namespace BTimeLogger.Wpf.Controls;

public interface ICreateNewIntervalViewModelFactory
{
	CreateNewIntervalViewModel Create();
}

class CreateNewIntervalViewModelFactory : ICreateNewIntervalViewModelFactory
{
	private readonly IActivityTypeSelectorViewModelFactory _activityTypeSelectorViewModelFactory;
	private readonly IMediator _mediator;

	public CreateNewIntervalViewModelFactory(IActivityTypeSelectorViewModelFactory activityTypeSelectorViewModelFactory,
		IMediator mediator)
	{
		_activityTypeSelectorViewModelFactory = activityTypeSelectorViewModelFactory;
		_mediator = mediator;
	}

	public CreateNewIntervalViewModel Create()
	{
		return new CreateNewIntervalViewModel(_activityTypeSelectorViewModelFactory.Create(), _mediator);
	}
}
