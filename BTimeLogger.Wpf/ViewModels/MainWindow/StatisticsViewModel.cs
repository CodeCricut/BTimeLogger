using BTimeLogger.Wpf.ViewModels.PieChart;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class StatisticsViewModel : BaseViewModel
	{
		public GroupStatisticsPieChartViewModel GroupStatisticsPieChartViewModel { get; }

		public GroupFilterViewModel GroupFilterViewModel { get; }

		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }


		public StatisticsViewModel(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
			GroupFilterViewModel groupFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			GroupStatisticsPieChartViewModel = groupStatisticsPieChartViewModel;
			GroupFilterViewModel = groupFilterViewModel;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;
		}
	}
}
