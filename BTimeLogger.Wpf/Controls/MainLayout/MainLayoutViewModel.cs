using BTimeLogger.Wpf.Util;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class MainLayoutViewModel : BaseViewModel
	{
		private readonly IActivityViewModelFactory _activityViewModelFactory;

		public GroupedActivityFilterViewModel GroupedActivityFilterViewModel { get; }
		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }
		public PartialIntervalListViewModel PartialIntervalListViewModel { get; }
		public GroupStatisticsPieChartViewModel GroupStatisticsPieChartViewModel { get; }

		public CurrentReportBannerViewModel CurrentReportBannerViewModel { get; }

		public MainLayoutViewModel(IEventAggregator ea,
							 GroupedActivityFilterViewModel groupedActivityFilterViewModel,
							 TimeSpanPanelViewModel timeSpanPanelViewModel,
							 PartialIntervalListViewModel partialIntervalListViewModel,
							 GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
							 CurrentReportBannerViewModel currentReportBannerViewModel,
							 IActivityViewModelFactory activityViewModelFactory)
		{
			GroupedActivityFilterViewModel = groupedActivityFilterViewModel;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;
			PartialIntervalListViewModel = partialIntervalListViewModel;
			GroupStatisticsPieChartViewModel = groupStatisticsPieChartViewModel;
			CurrentReportBannerViewModel = currentReportBannerViewModel;
			_activityViewModelFactory = activityViewModelFactory;


			GroupedActivityFilterViewModel.NoGroupActivitySelected += (_, args) =>
				ea.SendMessage(IncludedIntervalActivitiesChanged.NoIncludedActivities());

			GroupedActivityFilterViewModel.GroupActivitySelected += (_, args) =>
				ea.SendMessage(IncludedIntervalActivitiesChanged.SingleActivity(GroupedActivityFilterViewModel.GroupsSource.SelectedGroupActivity.Activity.Code));

			GroupedActivityFilterViewModel.ActivitiesSelected += (_, args) =>
				ea.SendMessage(new IncludedIntervalActivitiesChanged(GroupedActivityFilterViewModel.SelectedActivities.SelectActivityCodes()));

			TimeSpanPanelViewModel.PropertyChanged += (_, _) =>
				ea.SendMessage(new IntervalsTimeSpanChanged(TimeSpanPanelViewModel.From, TimeSpanPanelViewModel.To));

			ea.SendMessage(new IntervalsTimeSpanChanged(TimeSpanPanelViewModel.From, TimeSpanPanelViewModel.To));


			GroupedActivityFilterViewModel.GroupsSource.PropertyChanged += (_, args) =>
			{
				if (GroupedActivityFilterViewModel.GroupsSource.NoActivityGroupSelected)
					ea.SendMessage(GroupStatisticsTypeChanged.NoGroup());
				else
					ea.SendMessage(new GroupStatisticsTypeChanged(GroupedActivityFilterViewModel.GroupsSource.SelectedGroupActivity.Activity));
			};

			TimeSpanPanelViewModel.PropertyChanged += (_, _) =>
				ea.SendMessage(new StatisticsTimeSpanChanged(TimeSpanPanelViewModel.From, TimeSpanPanelViewModel.To));

			ea.RegisterHandler<NewPieChartGroupSelected>(HandleNewPieChartGroupSelected);
		}

		private void HandleNewPieChartGroupSelected(NewPieChartGroupSelected msg)
		{
			ActivityViewModel selectedActivityVM = _activityViewModelFactory.Create(msg.SelectedGroup);
			GroupedActivityFilterViewModel.GroupsSource.SelectActivity(selectedActivityVM);
		}
	}
}