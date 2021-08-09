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
		private readonly IActivityViewModelFactory _activityViewModelFactory;

		public StatisticsViewModelFactory(IEventAggregator ea, IActivityViewModelFactory activityViewModelFactory)
		{
			_ea = ea;
			_activityViewModelFactory = activityViewModelFactory;
		}
		public StatisticsViewModel Create(GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
			GroupFilterViewModel groupFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			return new StatisticsViewModel(groupStatisticsPieChartViewModel, groupFilterViewModel, timeSpanPanelViewModel, _ea, _activityViewModelFactory);
		}
	}
}
