using BTimeLogger.Wpf.ViewModels.MainWindow;
using BTimeLogger.Wpf.ViewModels.PieChart;
using WpfCore.MessageBus;

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
		private readonly IEventAggregator _ea;

		public StatisticsViewModelFactory(IEventAggregator ea)
		{
			_ea = ea;
		}
		public StatisticsViewModel Create(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
			GroupFilterViewModel groupFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			return new StatisticsViewModel(groupStatisticsPieChartViewModel, groupFilterViewModel, timeSpanPanelViewModel, _ea);
		}
	}
}
