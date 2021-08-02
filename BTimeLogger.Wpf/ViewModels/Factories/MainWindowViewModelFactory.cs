using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
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

		public MainWindowViewModelFactory(
			IWindowButtonsViewModelFactory windowButtonsViewModel,
			IMainLayoutViewModelFactory mainLayoutViewModelFactory,
			ITitleBarMenuViewModelFactory titleBarMenuViewModel)
		{
			_windowButtonsViewModelFactory = windowButtonsViewModel;
			_mainLayoutViewModelFactory = mainLayoutViewModelFactory;
			_titleBarMenuViewModel = titleBarMenuViewModel;
		}

		public MainWindowViewModel Create()
		{
			WindowButtonsViewModel windowButtonsVM = _windowButtonsViewModelFactory.Create();
			MainLayoutViewModel mainLayoutVM = _mainLayoutViewModelFactory.Create();
			TitleBarMenuViewModel titleBarMenuVM = _titleBarMenuViewModel.Create();
			return new MainWindowViewModel(windowButtonsVM, mainLayoutVM, titleBarMenuVM);
		}
	}
}
