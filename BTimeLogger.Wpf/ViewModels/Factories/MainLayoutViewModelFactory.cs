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
		private readonly ICreateReportWindowViewModelFactory _createReportWindowViewModelFactory;
		private readonly IIntervalListViewModelFactory _intervalListViewModelFactory;
		private readonly IGroupedActivityFilterViewModelFactory _groupedActivityFilterViewModel;
		private readonly ITimeSpanPanelViewModelFactory _timeSpanPanelViewModelFactory;

		public MainLayoutViewModelFactory(
			IViewManager viewManager,
			IHomeViewModelFactory homeViewModelFactory,
			IIntervalsViewModelFactory intervalsViewModelFactory,
			IStatisticsViewModelFactory statisticsViewModelFactory,
			ICreateReportWindowViewModelFactory createReportWindowViewModelFactory,
			IIntervalListViewModelFactory intervalListViewModelFactory,
			IGroupedActivityFilterViewModelFactory groupedActivityFilterViewModel,
			ITimeSpanPanelViewModelFactory timeSpanPanelViewModelFactory)
		{
			_viewManager = viewManager;
			_homeViewModelFactory = homeViewModelFactory;
			_intervalsViewModelFactory = intervalsViewModelFactory;
			_statisticsViewModelFactory = statisticsViewModelFactory;
			_createReportWindowViewModelFactory = createReportWindowViewModelFactory;
			_intervalListViewModelFactory = intervalListViewModelFactory;
			_groupedActivityFilterViewModel = groupedActivityFilterViewModel;
			_timeSpanPanelViewModelFactory = timeSpanPanelViewModelFactory;
		}

		public MainLayoutViewModel Create()
		{
			var homeVM = _homeViewModelFactory.Create();
			var intervalListVM = _intervalListViewModelFactory.Create();
			var groupedActivityFilterVM = _groupedActivityFilterViewModel.Create();
			var timeSpanPanelVM = _timeSpanPanelViewModelFactory.Create();
			var intervalsVM = _intervalsViewModelFactory.Create(intervalListVM, groupedActivityFilterVM, timeSpanPanelVM);
			var statsVM = _statisticsViewModelFactory.Create();

			return new MainLayoutViewModel(homeVM, intervalsVM, statsVM, _viewManager, _createReportWindowViewModelFactory);
		}
	}
}
