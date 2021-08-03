using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.ViewModels.Factories;
using BTimeLogger.Wpf.ViewModels.Messages;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class IntervalsViewModel : BaseViewModel
	{
		private readonly ICreateNewIntervalWindowViewModelFactory _createNewIntervalWindowViewModelFactory;
		private readonly IViewManager _viewManager;

		public PartialIntervalListViewModel PartialIntervalListViewModel { get; }
		public GroupedActivityFilterViewModel GroupedActivityFilterViewModel { get; }
		public TimeSpanPanelViewModel TimeSpanPanelViewModel { get; }

		public AsyncDelegateCommand CreateNewIntervalCommand { get; }

		public IntervalsViewModel(
			PartialIntervalListViewModel partialIntervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterVM,
			TimeSpanPanelViewModel timeSpanPanelViewModel,
			ICreateNewIntervalWindowViewModelFactory createNewIntervalWindowViewModelFactory,
			IViewManager viewManager,
			IEventAggregator ea)
		{
			PartialIntervalListViewModel = partialIntervalListViewModel;
			GroupedActivityFilterViewModel = groupedActivityFilterVM;
			TimeSpanPanelViewModel = timeSpanPanelViewModel;
			_createNewIntervalWindowViewModelFactory = createNewIntervalWindowViewModelFactory;
			_viewManager = viewManager;
			GroupedActivityFilterViewModel.NoGroupActivitySelected += (_, args) =>
				ea.SendMessage(IncludedIntervalActivitiesChanged.NoIncludedActivities());

			GroupedActivityFilterViewModel.GroupActivitySelected += (_, args) =>
				ea.SendMessage(IncludedIntervalActivitiesChanged.SingleActivity(GroupedActivityFilterViewModel.GroupsSource.SelectedGroupActivity.Activity.Code));

			GroupedActivityFilterViewModel.ActivitiesSelected += (_, args) =>
				ea.SendMessage(new IncludedIntervalActivitiesChanged(GroupedActivityFilterViewModel.SelectedActivities.SelectActivityCodes()));

			CreateNewIntervalCommand = new AsyncDelegateCommand(CreateNewInterval);
		}

		private Task CreateNewInterval(object arg)
		{
			var createIntervalWindowVM = _createNewIntervalWindowViewModelFactory.Create();
			_viewManager.Show(createIntervalWindowVM);

			return Task.CompletedTask;
		}
	}
}
