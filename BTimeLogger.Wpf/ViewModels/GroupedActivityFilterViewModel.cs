using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.ViewModels.Domain;
using BTimeLogger.Wpf.ViewModels.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;
using static BTimeLogger.Wpf.ViewModels.ActivityGroupSourceViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class GroupedActivityFilterViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IMediator _mediator;

		private IEnumerable<ActivityViewModel> _allActivityVMs;


		public ActivityGroupSourceViewModel GroupsSource { get; } = new();

		public ObservableCollection<ActivityViewModel> ActivitiesSource { get; } = new();

		public ObservableCollection<ActivityViewModel> SelectedActivities { get; } = new();

		public AsyncDelegateCommand ReloadCommand { get; }

		public GroupedActivityFilterViewModel(
			IEventAggregator ea,
			IMediator mediator)
		{
			_ea = ea;
			_mediator = mediator;

			ReloadCommand = new AsyncDelegateCommand(Reload);

			ea.RegisterHandler<ReportSourceChanged>(msg => ReloadCommand.Execute());

			GroupsSource.PropertyChanged += (_, args) =>
			{
				if (GroupsSource.NoActivityGroupSelected)
				{
					_ea.SendMessage(IncludedActivitiesChanged.NoIncludedActivities());
					RePopulateActivities();
				}
				else
				{
					_ea.SendMessage(IncludedActivitiesChanged.SingleActivity(GroupsSource.SelectedGroupActivity.Activity.Code));
					RePopulateActivities();
				}
			};

			SelectedActivities.CollectionChanged += (object _, NotifyCollectionChangedEventArgs e) =>
				_ea.SendMessage(new IncludedActivitiesChanged(SelectedActivities.SelectActivityCodes()));

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
			GroupsSource.Items.Clear();
			foreach (ActivityViewModel activity in _allActivityVMs)
			{
				if (activity.IsGroup) GroupsSource.Items.Add(activity);
			}
			GroupsSource.Items.Add(new NoneItem());
		}

		private void RePopulateActivities()
		{
			if (GroupsSource.NoActivityGroupSelected)
				PopulateParentlessActivities();
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
				if (GroupsSource.SelectedGroupActivity.Activity.Children.Contains(activityVM.Activity) &&
					!activityVM.IsGroup)
				{
					ActivitiesSource.Add(activityVM);
				}
			}
		}
	}
}
