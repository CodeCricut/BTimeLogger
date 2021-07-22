using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class IntervalsViewModel : BaseViewModel
	{
		public GroupedActivityFilterViewModel GroupedActivityFilterViewModel { get; }
		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }
		public IntervalListViewModel IntervalListViewModel { get; }

		public IntervalsViewModel(IntervalListViewModel intervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterVM,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			IntervalListViewModel = intervalListViewModel;
			GroupedActivityFilterViewModel = groupedActivityFilterVM;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;
		}
	}
}
