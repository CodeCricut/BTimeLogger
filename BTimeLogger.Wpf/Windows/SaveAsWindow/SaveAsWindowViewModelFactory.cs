using MediatR;

namespace BTimeLogger.Wpf.Windows;

public interface ISaveAsWindowViewModelFactory
{
	SaveAsWindowViewModel Create();
}
class SaveAsWindowViewModelFactory : ISaveAsWindowViewModelFactory
{
	private readonly IMediator _mediator;
	private readonly IViewManager _viewManager;

	public SaveAsWindowViewModelFactory(IMediator mediator,
		IViewManager viewManager)
	{
		_mediator = mediator;
		_viewManager = viewManager;
	}

	public SaveAsWindowViewModel Create()
	{
		return new SaveAsWindowViewModel(_mediator, _viewManager);
	}
}
