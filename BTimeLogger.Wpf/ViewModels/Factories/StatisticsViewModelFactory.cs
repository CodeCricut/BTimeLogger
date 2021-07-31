using BTimeLogger.Wpf.ViewModels.MainWindow;
using BTimeLogger.Wpf.ViewModels.PieChart;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IStatisticsViewModelFactory
	{
		StatisticsViewModel Create(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
			GroupFilterViewModel groupFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel);
	}

	class StatisticsViewModelFactory : IStatisticsViewModelFactory
	{
		public StatisticsViewModel Create(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
			GroupFilterViewModel groupFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			return new StatisticsViewModel(groupStatisticsPieChartViewModel, groupFilterViewModel, timeSpanPanelViewModel);
		}
	}
}
