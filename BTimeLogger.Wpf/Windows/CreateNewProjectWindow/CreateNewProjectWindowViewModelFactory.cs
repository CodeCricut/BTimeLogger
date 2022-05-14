using MediatR;

namespace BTimeLogger.Wpf.Windows;

public interface ICreateNewProjectWindowViewModelFactory
{
	CreateNewProjectWindowViewModel Create();
}

class CreateNewProjectWindowViewModelFactory : ICreateNewProjectWindowViewModelFactory
{
	private readonly IMediator _mediator;
	private readonly IViewManager _viewManager;

	public CreateNewProjectWindowViewModelFactory(IMediator mediator, IViewManager viewManager)
	{
		_mediator = mediator;
		_viewManager = viewManager;
	}

	public CreateNewProjectWindowViewModel Create()
	{
		return new CreateNewProjectWindowViewModel(_mediator, _viewManager);
	}
}
