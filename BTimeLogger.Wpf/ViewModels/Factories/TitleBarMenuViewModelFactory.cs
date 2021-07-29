using BTimeLogger.Wpf.ViewModels.MainWindow;
using MediatR;
using WpfCore.Services;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface ITitleBarMenuViewModelFactory
	{
		TitleBarMenuViewModel Create();
	}

	class TitleBarMenuViewModelFactory : ITitleBarMenuViewModelFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IOpenCsvsWindowViewModelFactory _openCsvsWindowViewModelFactory;
		private readonly IMediator _mediator;

		public TitleBarMenuViewModelFactory(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory openCsvsWindowViewModelFactory,
			IMediator mediator)
		{
			_viewManager = viewManager;
			_openCsvsWindowViewModelFactory = openCsvsWindowViewModelFactory;
			_mediator = mediator;
		}

		public TitleBarMenuViewModel Create()
		{
			return new TitleBarMenuViewModel(_viewManager, _openCsvsWindowViewModelFactory, _mediator);
		}
	}
}
