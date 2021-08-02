using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.ViewModels.Messages;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class IntervalsViewModel : BaseViewModel
	{
		public PartialIntervalListViewModel PartialIntervalListViewModel { get; }
		public GroupedActivityFilterViewModel GroupedActivityFilterViewModel { get; }
		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }

		public IntervalsViewModel(
			PartialIntervalListViewModel partialIntervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterVM,
			TimeSpanPanelViewModel timeSpanPanelViewModel,
			IEventAggregator ea)
		{
			PartialIntervalListViewModel = partialIntervalListViewModel;
			GroupedActivityFilterViewModel = groupedActivityFilterVM;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;

			GroupedActivityFilterViewModel.NoGroupActivitySelected += (_, args) =>
				ea.SendMessage(IncludedIntervalActivitiesChanged.NoIncludedActivities());

			GroupedActivityFilterViewModel.GroupActivitySelected += (_, args) =>
				ea.SendMessage(IncludedIntervalActivitiesChanged.SingleActivity(GroupedActivityFilterViewModel.GroupsSource.SelectedGroupActivity.Activity.Code));

			GroupedActivityFilterViewModel.ActivitiesSelected += (_, args) =>
				ea.SendMessage(new IncludedIntervalActivitiesChanged(GroupedActivityFilterViewModel.SelectedActivities.SelectActivityCodes()));
		}
	}
}
