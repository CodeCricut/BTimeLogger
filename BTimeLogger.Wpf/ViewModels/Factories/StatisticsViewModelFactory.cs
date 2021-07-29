using BTimeLogger.Wpf.ViewModels.MainWindow;
using BTimeLogger.Wpf.ViewModels.PieChart;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IStatisticsViewModelFactory
	{
		StatisticsViewModel Create(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel);
	}

	class StatisticsViewModelFactory : IStatisticsViewModelFactory
	{
		public StatisticsViewModel Create(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel)
		{
			return new StatisticsViewModel(groupStatisticsPieChartViewModel);
		}
	}
}
