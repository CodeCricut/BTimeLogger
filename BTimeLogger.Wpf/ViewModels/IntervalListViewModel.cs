using BTimeLogger.Wpf.ViewModels.Factories;
using BTimeLogger.Wpf.ViewModels.Messages;
using System;
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
		private readonly IEventAggregator _ea;
		private readonly IIntervalRepository _intervalRepository;
		private readonly IIntervalListItemViewModelFactory _intervalItemVMFactory;

		public ObservableCollection<IntervalListItemViewModel> Items { get; } = new();

		// //////////////////////////// SEARCH FILTER VM
		private Activity[] _includedActivities;
		public Activity[] IncludedActivities
		{
			get { return _includedActivities; } // TODO
			set { _includedActivities = value; UpdateItemsCommand.Execute(); }
		}

		private DateTime _from;
		public DateTime From
		{
			get { return _from; }
			set { _from = value; UpdateItemsCommand.Execute(); }
		}

		private DateTime _to;
		public DateTime To
		{
			get { return _to; }
			set { _to = value; UpdateItemsCommand.Execute(); }
		}

		// TODO
		private bool _loading;
		public bool Loading
		{
			get { return _loading; }
			set { _loading = value; }
		}

		// TODO
		public bool IsEmpty;

		public AsyncDelegateCommand UpdateItemsCommand { get; }

		public IntervalListViewModel(IEventAggregator ea,
			IIntervalRepository intervalRepository,
			IIntervalListItemViewModelFactory intervalItemVMFactory
			)
		{
			_ea = ea;
			_intervalRepository = intervalRepository;
			_intervalItemVMFactory = intervalItemVMFactory;

			UpdateItemsCommand = new AsyncDelegateCommand(UpdateItems);

			ea.RegisterHandler<CsvLocationChanged>(msg => UpdateItemsCommand.Execute());
			ea.RegisterHandler<IncludedActivitiesChanged>(msg => IncludedActivities = msg.NewIncludedActivities);
			ea.RegisterHandler<SearchBetweenDatesChanged>(msg => { From = msg.From; To = msg.To; });
		}

		private async Task UpdateItems(object _ = null)
		{
			Interval[] intervals = (await _intervalRepository.GetIntervals(IncludedActivities, From, To)).ToArray();

			Items.Clear();
			for (int i = 0; i < intervals.Length; i++)
			{
				Interval interval = intervals[i];
				IntervalListItemViewModel intervalItem = _intervalItemVMFactory.Create(interval, isLastOnDate: false); // TODO: determine whether last on date
				Items.Add(intervalItem);
			}
		}
	}
}
