using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Controls
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
