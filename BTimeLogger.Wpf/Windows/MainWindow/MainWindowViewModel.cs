using BTimeLogger.Wpf.Controls;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Windows
{
	public class MainWindowViewModel : BaseViewModel
	{
		public WindowButtonsViewModel WindowButtonsViewModel { get; set; }
		public MainLayoutViewModel MainLayoutViewModel { get; }
		public TitleBarMenuViewModel TitleBarMenuViewModel { get; }

		public MainWindowViewModel(
			WindowButtonsViewModel windowButtonsViewModel,
			MainLayoutViewModel mainLayoutViewModel,
			TitleBarMenuViewModel titleBarMenuViewModel)
		{
			WindowButtonsViewModel = windowButtonsViewModel;
			MainLayoutViewModel = mainLayoutViewModel;
			TitleBarMenuViewModel = titleBarMenuViewModel;
		}
	}
}
