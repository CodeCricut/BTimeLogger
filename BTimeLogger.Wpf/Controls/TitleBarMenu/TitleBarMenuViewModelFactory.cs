using BTimeLogger.Wpf.Windows;
using MediatR;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Controls
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
		private readonly ICreateNewActivityWindowViewModelFactory _createNewActivityWindowViewModelFactory;
		private readonly ISaveAsWindowViewModelFactory _saveAsWindowViewModelFactory;

		public TitleBarMenuViewModelFactory(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory openCsvsWindowViewModelFactory,
			IMediator mediator,
			ICreateNewIntervalWindowViewModelFactory createNewIntervalWindowViewModelFactory,
			ICreateNewActivityWindowViewModelFactory createNewActivityWindowViewModelFactory,
			ISaveAsWindowViewModelFactory saveAsWindowViewModelFactory)
		{
			_viewManager = viewManager;
			_openCsvsWindowViewModelFactory = openCsvsWindowViewModelFactory;
			_mediator = mediator;
			_createNewIntervalWindowViewModelFactory = createNewIntervalWindowViewModelFactory;
			_createNewActivityWindowViewModelFactory = createNewActivityWindowViewModelFactory;
			_saveAsWindowViewModelFactory = saveAsWindowViewModelFactory;
		}

		public TitleBarMenuViewModel Create()
		{
			return new TitleBarMenuViewModel(_viewManager, _openCsvsWindowViewModelFactory, _createNewIntervalWindowViewModelFactory,
				_createNewActivityWindowViewModelFactory, _saveAsWindowViewModelFactory, _mediator);
		}
	}
}
