using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.ViewModels.Domain;
using BTimeLogger.Wpf.ViewModels.Messages;
using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class GroupedActivityFilterViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IMediator _mediator;

		private IEnumerable<ActivityViewModel> _allActivityVMs;

		public ObservableCollection<ActivityViewModel> GroupsSource { get; } = new();
		public ObservableCollection<ActivityViewModel> ActivitiesSource { get; } = new();

		private ActivityViewModel _selectedGroupActivity;
		public ActivityViewModel SelectedGroupActivity
		{
			get => _selectedGroupActivity;
			set => Set(ref _selectedGroupActivity, value);
		}

		public ObservableCollection<ActivityViewModel> SelectedActivities { get; } = new();

		public AsyncDelegateCommand ReloadCommand { get; }

		public GroupedActivityFilterViewModel(
			IEventAggregator ea,
			IMediator mediator)
		{
			_ea = ea;
			_mediator = mediator;

			ReloadCommand = new AsyncDelegateCommand(Reload);

			ea.RegisterHandler<GlobalDataSourceChanged>(msg => ReloadCommand.Execute());

			PropertyChanged += (_, eventArgs) =>
				{
					if (eventArgs.PropertyName.Equals(nameof(SelectedGroupActivity)))
						RePopulateActivities();
				};

			SelectedActivities.CollectionChanged += (object _, NotifyCollectionChangedEventArgs e) =>
					_ea.SendMessage(new IncludedActivitiesChanged(SelectedActivities.SelectActivities().ToArray()));

			ReloadCommand.Execute();
		}


		private async Task Reload(object param = null)
		{
			_allActivityVMs = await _mediator.Send(new GetAllActivityVMsQuery());
			PopulateSources();
		}

		private void PopulateSources()
		{
			PopulateGroups();
			PopulateParentlessActivities();
		}

		private void PopulateGroups()
		{
			GroupsSource.Clear();
			foreach (ActivityViewModel activity in _allActivityVMs)
			{
				if (activity.IsGroup) GroupsSource.Add(activity);
			}
		}

		private void RePopulateActivities()
		{
			if (SelectedGroupActivity == null) PopulateParentlessActivities();
			else PopulateActivitiesOfSelectedGroup();
		}

		private void PopulateParentlessActivities()
		{
			ActivitiesSource.Clear();
			foreach (ActivityViewModel activity in _allActivityVMs)
			{
				if (!activity.IsGroup &&
					!activity.HasParent)
					ActivitiesSource.Add(activity);
			}
		}

		private void PopulateActivitiesOfSelectedGroup()
		{
			ActivitiesSource.Clear();
			foreach (
				ActivityViewModel activityVM in _allActivityVMs)
			{
				if (SelectedGroupActivity.Activity.Children.Contains(activityVM.Activity) &&
					!activityVM.IsGroup)
				{
					ActivitiesSource.Add(activityVM);
				}
			}
		}
	}
}
