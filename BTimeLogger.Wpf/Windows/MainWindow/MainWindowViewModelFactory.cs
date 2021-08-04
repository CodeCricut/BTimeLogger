using BTimeLogger.Wpf.Controls;
using MediatR;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Windows
{
	public interface IMainWindowViewModelFactory
	{
		MainWindowViewModel Create();
	}

	class MainWindowViewModelFactory : IMainWindowViewModelFactory
	{
		private readonly IWindowButtonsViewModelFactory _windowButtonsViewModelFactory;
		private readonly IMainLayoutViewModelFactory _mainLayoutViewModelFactory;
		private readonly ITitleBarMenuViewModelFactory _titleBarMenuViewModel;
		private readonly IMediator _mediator;
		private readonly IViewManager _viewManager;

		public MainWindowViewModelFactory(
			IWindowButtonsViewModelFactory windowButtonsViewModel,
			IMainLayoutViewModelFactory mainLayoutViewModelFactory,
			ITitleBarMenuViewModelFactory titleBarMenuViewModel,
			IMediator mediator,
			IViewManager viewManager)
		{
			_windowButtonsViewModelFactory = windowButtonsViewModel;
			_mainLayoutViewModelFactory = mainLayoutViewModelFactory;
			_titleBarMenuViewModel = titleBarMenuViewModel;
			_mediator = mediator;
			_viewManager = viewManager;
		}

		public MainWindowViewModel Create()
		{
			WindowButtonsViewModel windowButtonsVM = _windowButtonsViewModelFactory.Create();
			MainLayoutViewModel mainLayoutVM = _mainLayoutViewModelFactory.Create();
			TitleBarMenuViewModel titleBarMenuVM = _titleBarMenuViewModel.Create();
			return new MainWindowViewModel(windowButtonsVM, mainLayoutVM, titleBarMenuVM, _mediator, _viewManager);
		}
	}
}
