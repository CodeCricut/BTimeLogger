using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class MainLayoutViewModel : BaseViewModel
	{
		public GroupedActivityFilterViewModel GroupedActivityFilterViewModel { get; }
		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }
		public PartialIntervalListViewModel PartialIntervalListViewModel { get; }
		public GroupStatisticsPieChartViewModel GroupStatisticsPieChartViewModel { get; }

		public CurrentReportBannerViewModel CurrentReportBannerViewModel { get; }

		public MainLayoutViewModel(GroupedActivityFilterViewModel groupedActivityFilterViewModel,
							 TimeSpanPanelViewModel timeSpanPanelViewModel,
							 PartialIntervalListViewModel partialIntervalListViewModel,
							 GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
							 CurrentReportBannerViewModel currentReportBannerViewModel)
		{
			GroupedActivityFilterViewModel = groupedActivityFilterViewModel;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;
			PartialIntervalListViewModel = partialIntervalListViewModel;
			GroupStatisticsPieChartViewModel = groupStatisticsPieChartViewModel;
			CurrentReportBannerViewModel = currentReportBannerViewModel;
		}
	}
}