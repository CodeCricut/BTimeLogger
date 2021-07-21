using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class IntervalsViewModel : BaseViewModel
	{
		public IntervalListViewModel IntervalListViewModel { get; }

		public IntervalsViewModel(IntervalListViewModel intervalListViewModel)
		{
			IntervalListViewModel = intervalListViewModel;
		}
	}
}
