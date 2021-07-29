using BTimeLogger.Wpf.ViewModels.PieChart;
using WpfCore.Services;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IMainLayoutViewModelFactory
	{
		MainLayoutViewModel Create();
	}

	class MainLayoutViewModelFactory : IMainLayoutViewModelFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IHomeViewModelFactory _homeViewModelFactory;
		private readonly IIntervalsViewModelFactory _intervalsViewModelFactory;
		private readonly IStatisticsViewModelFactory _statisticsViewModelFactory;
		private readonly IOpenCsvsWindowViewModelFactory _createReportWindowViewModelFactory;
		private readonly IIntervalListViewModelFactory _intervalListViewModelFactory;
		private readonly IGroupedActivityFilterViewModelFactory _groupedActivityFilterViewModel;
		private readonly ITimeSpanPanelViewModelFactory _timeSpanPanelViewModelFactory;
		private readonly IPaginatedIntervalListViewModelFactory _paginatedIntervalListViewModel;
		private readonly IPartialIntervalListViewModelFactory _partialIntervalListViewModel;
		private readonly IGroupStatisticsPieChartViewModelFactory _groupStatisticsPieChartViewModelFactory;

		public MainLayoutViewModelFactory(
			IViewManager viewManager,
			IHomeViewModelFactory homeViewModelFactory,
			IIntervalsViewModelFactory intervalsViewModelFactory,
			IStatisticsViewModelFactory statisticsViewModelFactory,
			IOpenCsvsWindowViewModelFactory createReportWindowViewModelFactory,
			IIntervalListViewModelFactory intervalListViewModelFactory,
			IGroupedActivityFilterViewModelFactory groupedActivityFilterViewModel,
			ITimeSpanPanelViewModelFactory timeSpanPanelViewModelFactory,
			IPaginatedIntervalListViewModelFactory paginatedIntervalListViewModel,
			IPartialIntervalListViewModelFactory partialIntervalListViewModel,
			IGroupStatisticsPieChartViewModelFactory groupStatisticsPieChartViewModelFactory)
		{
			_viewManager = viewManager;
			_homeViewModelFactory = homeViewModelFactory;
			_intervalsViewModelFactory = intervalsViewModelFactory;
			_statisticsViewModelFactory = statisticsViewModelFactory;
			_createReportWindowViewModelFactory = createReportWindowViewModelFactory;
			_intervalListViewModelFactory = intervalListViewModelFactory;
			_groupedActivityFilterViewModel = groupedActivityFilterViewModel;
			_timeSpanPanelViewModelFactory = timeSpanPanelViewModelFactory;
			_paginatedIntervalListViewModel = paginatedIntervalListViewModel;
			_partialIntervalListViewModel = partialIntervalListViewModel;
			_groupStatisticsPieChartViewModelFactory = groupStatisticsPieChartViewModelFactory;
		}

		public MainLayoutViewModel Create()
		{
			var homeVM = _homeViewModelFactory.Create();
			//var intervalListVM = _intervalListViewModelFactory.Create();
			var groupedActivityFilterVM = _groupedActivityFilterViewModel.Create();
			var timeSpanPanelVM = _timeSpanPanelViewModelFactory.Create();

			var paginatedIntervalListVM = _paginatedIntervalListViewModel.Create();
			var partialIntervalListVM = _partialIntervalListViewModel.Create(paginatedIntervalListVM);

			var intervalsVM = _intervalsViewModelFactory.Create(partialIntervalListVM, groupedActivityFilterVM, timeSpanPanelVM);

			GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel = _groupStatisticsPieChartViewModelFactory.Create();
			var statsVM = _statisticsViewModelFactory.Create(groupStatisticsPieChartViewModel);

			return new MainLayoutViewModel(homeVM, intervalsVM, statsVM);
		}
	}
}
