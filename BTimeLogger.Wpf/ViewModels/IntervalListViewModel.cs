using BTimeLogger.Wpf.Model;
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

		public ObservableCollection<IntervalListItemViewModel> Items { get; } = new();

		public IntervalSearchFilter IntervalSearchFilter { get; } = new();

		private bool _loading;
		public bool Loading { get => _loading; set { Set(ref _loading, value); RaisePropertyChanged(nameof(NotLoading)); } }
		public bool NotLoading { get => !Loading; }

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

			ea.RegisterHandler<CsvLocationChanged>(msg => UpdateItemsCommand.Execute());
			ea.RegisterHandler<IncludedActivitiesChanged>(msg => IntervalSearchFilter.IncludedActivities = msg.NewIncludedActivities);
			ea.RegisterHandler<SearchBetweenDatesChanged>(msg => { IntervalSearchFilter.From = msg.From; IntervalSearchFilter.To = msg.To; });
		}

		private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(ALL_PROPS_CHANGED);
		}

		private async Task UpdateItems(object _ = null)
		{
			Loading = true;

			Items.Clear();

			Interval[] intervals = (await _intervalRepository
				.GetIntervals(
					IntervalSearchFilter.IncludedActivities,
					IntervalSearchFilter.From,
					IntervalSearchFilter.To))
				.ToArray();


			Loading = false;
			for (int i = 0; i < intervals.Length; i++)
			{
				Interval interval = intervals[i];
				bool isLast = interval.IsLastOnDate(intervals);
				IntervalListItemViewModel intervalItem = _intervalItemVMFactory.Create(interval, isLast);
				Items.Add(intervalItem);
			}
		}
	}
}
