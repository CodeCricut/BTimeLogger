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
		private readonly ICreateNewIntervalWindowViewModelFactory _createNewIntervalWindowViewModelFactory;
		private readonly ISaveAsWindowViewModelFactory _saveAsWindowViewModelFactory;

		public TitleBarMenuViewModelFactory(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory openCsvsWindowViewModelFactory,
			IMediator mediator,
			ICreateNewIntervalWindowViewModelFactory createNewIntervalWindowViewModelFactory,
			ISaveAsWindowViewModelFactory saveAsWindowViewModelFactory)
		{
			_viewManager = viewManager;
			_openCsvsWindowViewModelFactory = openCsvsWindowViewModelFactory;
			_mediator = mediator;
			_createNewIntervalWindowViewModelFactory = createNewIntervalWindowViewModelFactory;
			_saveAsWindowViewModelFactory = saveAsWindowViewModelFactory;
		}

		public TitleBarMenuViewModel Create()
		{
			return new TitleBarMenuViewModel(_viewManager, _openCsvsWindowViewModelFactory, _createNewIntervalWindowViewModelFactory,
				_saveAsWindowViewModelFactory, _mediator);
		}
	}
}
