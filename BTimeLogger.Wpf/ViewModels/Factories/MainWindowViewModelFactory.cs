namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IMainWindowViewModelFactory
	{
		MainWindowViewModel Create();
	}

	class MainWindowViewModelFactory : IMainWindowViewModelFactory
	{
		private readonly IWindowButtonsViewModelFactory _windowButtonsViewModel;
		private readonly IMainLayoutViewModelFactory _mainLayoutViewModelFactory;

		public MainWindowViewModelFactory(
			IWindowButtonsViewModelFactory windowButtonsViewModel,
			IMainLayoutViewModelFactory mainLayoutViewModelFactory)
		{
			_windowButtonsViewModel = windowButtonsViewModel;
			_mainLayoutViewModelFactory = mainLayoutViewModelFactory;
		}

		public MainWindowViewModel Create()
		{
			WindowButtonsViewModel windowButtonsVM = _windowButtonsViewModel.Create();
			MainLayoutViewModel mainLayoutVM = _mainLayoutViewModelFactory.Create();
			return new(windowButtonsVM, mainLayoutVM);
		}
	}
}
