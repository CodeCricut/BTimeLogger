﻿using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.ViewModels.Factories;
using BTimeLogger.Wpf.ViewModels.Messages;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class IntervalListViewModel : BaseViewModel
	{
		private readonly IIntervalRepository _intervalRepository;
		private readonly IIntervalListItemViewModelFactory _intervalItemVMFactory;

		private readonly IntervalSearchFilter _intervalSearchFilter = new();


		public ObservableCollection<IntervalListItemViewModel> Items { get; } = new();

		#region Loading
		private bool _loading;
		public bool Loading
		{
			get => _loading;
			set
			{
				Set(ref _loading, value);
				RaisePropertyChanged(nameof(NotLoading));
			}
		}
		public bool NotLoading { get => !Loading; }
		#endregion

		public bool IsEmpty { get => Items.Count <= 0 && NotLoading; }

		public AsyncDelegateCommand UpdateItemsCommand { get; }

		public IntervalListViewModel(IEventAggregator ea,
			IIntervalRepository intervalRepository,
			IIntervalListItemViewModelFactory intervalItemVMFactory
			)
		{
			_intervalRepository = intervalRepository;
			_intervalItemVMFactory = intervalItemVMFactory;

			UpdateItemsCommand = new AsyncDelegateCommand(UpdateItems);

			Items.CollectionChanged += Items_CollectionChanged;

			ea.RegisterHandler<ReportSourceChanged>(msg => UpdateItemsCommand.Execute());
			ea.RegisterHandler<IncludedActivitiesChanged>(HandleIncludedActivitiesChanged);
			ea.RegisterHandler<TimeSpanChanged>(HandleSearchBetweenDatesChanged);
		}

		private async Task UpdateItems(object _ = null)
		{
			Loading = true;

			Items.Clear();

			IQueryable<Interval> intervals = await _intervalRepository
				.GetIntervals(
					_intervalSearchFilter.IncludedActivities,
					_intervalSearchFilter.From,
					_intervalSearchFilter.To);

			Loading = false;
			await Task.Factory.StartNew(() =>
			{
				foreach (var interval in intervals)
				{
					bool isLast = interval.IsLastOnDate(intervals);
					IntervalListItemViewModel intervalItem = _intervalItemVMFactory.Create(interval, isLast);

					App.Current.Dispatcher.Invoke(() => Items.Add(intervalItem));
				}
			});

			//for (int i = 0; i < intervals.Length; i++)
			//{
			//	Interval interval = intervals[i];
			//	bool isLast = interval.IsLastOnDate(intervals);
			//	IntervalListItemViewModel intervalItem = _intervalItemVMFactory.Create(interval, isLast);
			//	Items.Add(intervalItem);
			//}
		}

		private void HandleIncludedActivitiesChanged(IncludedActivitiesChanged msg)
		{
			_intervalSearchFilter.IncludedActivities = msg.NewIncludedActivities;
			UpdateItemsCommand.Execute();
		}

		private void HandleSearchBetweenDatesChanged(TimeSpanChanged msg)
		{
			_intervalSearchFilter.From = msg.From;
			_intervalSearchFilter.To = msg.To;
			UpdateItemsCommand.Execute();
		}

		private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(ALL_PROPS_CHANGED);
		}
	}
}
