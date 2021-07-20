using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public WindowButtonsViewModel WindowButtonsViewModel { get; set; }
		public MainLayoutViewModel MainLayoutViewModel { get; }

		public MainWindowViewModel(
			WindowButtonsViewModel windowButtonsViewModel,
			MainLayoutViewModel mainLayoutViewModel)
		{
			WindowButtonsViewModel = windowButtonsViewModel;
			MainLayoutViewModel = mainLayoutViewModel;
		}
	}
}
