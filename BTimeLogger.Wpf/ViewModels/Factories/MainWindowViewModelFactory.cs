namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IMainWindowViewModelFactory
	{
		MainWindowViewModel Create();
	}

	class MainWindowViewModelFactory : IMainWindowViewModelFactory
	{
		private readonly IWindowButtonsViewModelFactory _windowButtonsViewModel;

		public MainWindowViewModelFactory(IWindowButtonsViewModelFactory windowButtonsViewModel)
		{
			_windowButtonsViewModel = windowButtonsViewModel;
		}

		public MainWindowViewModel Create()
		{
			WindowButtonsViewModel windowButtonsVM = _windowButtonsViewModel.Create();
			return new(windowButtonsVM);
		}
	}
}
