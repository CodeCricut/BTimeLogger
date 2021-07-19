using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		// TODO: move to abstract WindowViewModel
		// Create BaseWindow view which responds to window button commands
		public WindowButtonsViewModel WindowButtonsViewModel { get; set; }

		public MainWindowViewModel(WindowButtonsViewModel windowButtonsViewModel)
		{
			WindowButtonsViewModel = windowButtonsViewModel;
		}
	}
}
