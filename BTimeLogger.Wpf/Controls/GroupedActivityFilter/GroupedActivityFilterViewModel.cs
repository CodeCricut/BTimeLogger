﻿using BTimeLogger.Wpf.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
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


		public event EventHandler NoGroupActivitySelected;
		public event EventHandler GroupActivitySelected;
		public event EventHandler ActivitiesSelected;

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
					NoGroupActivitySelected?.Invoke(this, args);
					//_ea.SendMessage(IncludedIntervalActivitiesChanged.NoIncludedActivities());
					RePopulateActivities();
				}
				else
				{
					GroupActivitySelected?.Invoke(this, args);
					//_ea.SendMessage(IncludedIntervalActivitiesChanged.SingleActivity(GroupsSource.SelectedGroupActivity.Activity.Code));
					RePopulateActivities();
				}
			};

			SelectedActivities.CollectionChanged += (_, args) => ActivitiesSelected?.Invoke(this, args);
			//SelectedActivities.CollectionChanged += (object _, NotifyCollectionChangedEventArgs e) =>
			//	_ea.SendMessage(new IncludedIntervalActivitiesChanged(SelectedActivities.SelectActivityCodes()));

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