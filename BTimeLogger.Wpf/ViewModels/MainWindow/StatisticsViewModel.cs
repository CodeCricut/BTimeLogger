using BTimeLogger.Wpf.ViewModels.PieChart;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class StatisticsViewModel : BaseViewModel
	{
		public GroupStatisticsPieChartViewModel GroupStatisticsPieChartViewModel { get; }

		public StatisticsViewModel(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel)
		{
			GroupStatisticsPieChartViewModel = groupStatisticsPieChartViewModel;
		}
	}
}
