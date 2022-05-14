using MediatR;

namespace BTimeLogger.Wpf.Windows;

public interface IOpenCsvsWindowViewModelFactory
{
	OpenCsvsWindowViewModel Create();
}

class OpenCsvsWindowViewModelFactory : IOpenCsvsWindowViewModelFactory
{
	private readonly IViewManager _viewManager;
	private readonly IEventAggregator _ea;
	private readonly IMediator _mediator;

	public OpenCsvsWindowViewModelFactory(
		IViewManager viewManager,
		IEventAggregator ea,
		IMediator mediator)
	{
		_viewManager = viewManager;
		_ea = ea;
		_mediator = mediator;
	}

	public OpenCsvsWindowViewModel Create()
	{
		return new OpenCsvsWindowViewModel(_viewManager, _mediator);
	}
}
