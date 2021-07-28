using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class IntervalsViewModel : BaseViewModel
	{
		public PartialIntervalListViewModel PartialIntervalListViewModel { get; }
		public GroupedActivityFilterViewModel GroupedActivityFilterViewModel { get; }
		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }

		public IntervalsViewModel(
			PartialIntervalListViewModel partialIntervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterVM,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			PartialIntervalListViewModel = partialIntervalListViewModel;
			GroupedActivityFilterViewModel = groupedActivityFilterVM;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;
		}
	}
}
