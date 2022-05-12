using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.Windows;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class MainLayoutViewModel : BaseViewModel
	{
		private readonly IActivityViewModelFactory _activityViewModelFactory;
		private readonly IViewManager _viewManager;
		private readonly ICreateNewIntervalWindowViewModelFactory _createNewIntervalWindowViewModelFactory;
		private readonly ICreateNewActivityWindowViewModelFactory _createNewActivityWindowViewModelFactory;

		public AsyncDelegateCommand CreateIntervalCommand { get; }
		public AsyncDelegateCommand CreateActivityCommand { get; }

		public GroupedActivityFilterViewModel GroupedActivityFilterViewModel { get; }
		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }
		public PartialIntervalListViewModel PartialIntervalListViewModel { get; }
		public GroupStatisticsPieChartViewModel GroupStatisticsPieChartViewModel { get; }

		public CurrentReportBannerViewModel CurrentReportBannerViewModel { get; }

		public MainLayoutViewModel(IEventAggregator ea,
							 IViewManager viewManager,
							 GroupedActivityFilterViewModel groupedActivityFilterViewModel,
							 TimeSpanPanelViewModel timeSpanPanelViewModel,
							 PartialIntervalListViewModel partialIntervalListViewModel,
							 GroupStatisticsPieChartViewModel groupStatisticsPieChartViewModel,
							 CurrentReportBannerViewModel currentReportBannerViewModel,
							 ICreateNewIntervalWindowViewModelFactory createNewIntervalWindowViewModelFactory,
							 ICreateNewActivityWindowViewModelFactory createNewActivityWindowViewModelFactory,
							 IActivityViewModelFactory activityViewModelFactory)
		{
			_viewManager = viewManager;
			GroupedActivityFilterViewModel = groupedActivityFilterViewModel;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;
			PartialIntervalListViewModel = partialIntervalListViewModel;
			GroupStatisticsPieChartViewModel = groupStatisticsPieChartViewModel;
			CurrentReportBannerViewModel = currentReportBannerViewModel;
			_createNewIntervalWindowViewModelFactory = createNewIntervalWindowViewModelFactory;
			_createNewActivityWindowViewModelFactory = createNewActivityWindowViewModelFactory;
			_activityViewModelFactory = activityViewModelFactory;


			CreateIntervalCommand = new AsyncDelegateCommand(CreateNewInterval);
			CreateActivityCommand = new AsyncDelegateCommand(CreateNewActivity);


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


		private Task CreateNewInterval(object arg)
		{
			CreateNewIntervalWindowViewModel createIntervalWindowVM = _createNewIntervalWindowViewModelFactory.Create();
			_viewManager.ShowDialog(createIntervalWindowVM);

			return Task.CompletedTask;
		}

		private Task CreateNewActivity(object arg)
		{
			CreateNewActivityWindowViewModel createActivityWindowVM = _createNewActivityWindowViewModelFactory.Create();
			_viewManager.ShowDialog(createActivityWindowVM);

			return Task.CompletedTask;
		}

		private void HandleNewPieChartGroupSelected(NewPieChartGroupSelected msg)
		{
			ActivityViewModel selectedActivityVM = _activityViewModelFactory.Create(msg.SelectedGroup);
			GroupedActivityFilterViewModel.GroupsSource.SelectActivity(selectedActivityVM);
		}
	}
}