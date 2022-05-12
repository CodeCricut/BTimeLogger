using BTimeLogger.Wpf.Windows;
using WpfCore.MessageBus;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Controls
{
	public interface IMainLayoutViewModelFactory
	{
		MainLayoutViewModel Create();
	}

	internal class MainLayoutViewModelFactory : IMainLayoutViewModelFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IGroupedActivityFilterViewModelFactory _groupedActivityFilterViewModel;
		private readonly ITimeSpanPanelViewModelFactory _timeSpanPanelViewModelFactory;
		private readonly IPaginatedIntervalListViewModelFactory _paginatedIntervalListViewModel;
		private readonly IPartialIntervalListViewModelFactory _partialIntervalListViewModel;
		private readonly IGroupStatisticsPieChartViewModelFactory _groupStatisticsPieChartViewModelFactory;
		private readonly ICurrentReportBannerViewModelFactory _currentReportBannerViewModel;
		private readonly IActivityViewModelFactory _activityViewModelFactory;
		private readonly ICreateNewIntervalWindowViewModelFactory _createNewIntervalWindowViewModelFactory;
		private readonly ICreateNewActivityWindowViewModelFactory _createNewActivityWindowViewModelFactory;

		public MainLayoutViewModelFactory(
			IEventAggregator ea,
			IViewManager viewManager,
			IGroupedActivityFilterViewModelFactory groupedActivityFilterViewModel,
			ITimeSpanPanelViewModelFactory timeSpanPanelViewModelFactory,
			IPaginatedIntervalListViewModelFactory paginatedIntervalListViewModel,
			IPartialIntervalListViewModelFactory partialIntervalListViewModel,
			IGroupStatisticsPieChartViewModelFactory groupStatisticsPieChartViewModelFactory,
			ICurrentReportBannerViewModelFactory currentReportBannerViewModel,
			IActivityViewModelFactory activityViewModelFactory,
			ICreateNewIntervalWindowViewModelFactory createNewIntervalWindowViewModelFactory,
			ICreateNewActivityWindowViewModelFactory createNewActivityWindowViewModelFactory)
		{
			_ea = ea;
			_viewManager = viewManager;
			_groupedActivityFilterViewModel = groupedActivityFilterViewModel;
			_timeSpanPanelViewModelFactory = timeSpanPanelViewModelFactory;
			_paginatedIntervalListViewModel = paginatedIntervalListViewModel;
			_partialIntervalListViewModel = partialIntervalListViewModel;
			_groupStatisticsPieChartViewModelFactory = groupStatisticsPieChartViewModelFactory;
			_currentReportBannerViewModel = currentReportBannerViewModel;
			_activityViewModelFactory = activityViewModelFactory;
			_createNewIntervalWindowViewModelFactory = createNewIntervalWindowViewModelFactory;
			_createNewActivityWindowViewModelFactory = createNewActivityWindowViewModelFactory;
		}

		public MainLayoutViewModel Create()
		{
			var groupedActivityFilterVM = _groupedActivityFilterViewModel.Create();
			var paginatedIntervalListVM = _paginatedIntervalListViewModel.Create();
			var partialIntervalListVM = _partialIntervalListViewModel.Create(paginatedIntervalListVM);

			var groupStatisticsPieChartViewModel = _groupStatisticsPieChartViewModelFactory.Create();
			var statTimeSpanPanelVM = _timeSpanPanelViewModelFactory.Create();

			CurrentReportBannerViewModel currentReportBannerVM = _currentReportBannerViewModel.Create();
			return new MainLayoutViewModel(_ea,
									_viewManager,
								  groupedActivityFilterVM,
								  statTimeSpanPanelVM,
								  partialIntervalListVM,
								  groupStatisticsPieChartViewModel,
								  currentReportBannerVM,
								  _createNewIntervalWindowViewModelFactory,
								  _createNewActivityWindowViewModelFactory,
								  _activityViewModelFactory);
		}
	}
}