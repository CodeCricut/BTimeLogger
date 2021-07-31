using BTimeLogger.Domain;
using BTimeLogger.Domain.Helpers;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Util;
using BTimeLogger.Wpf.ViewModels.Factories;
using BTimeLogger.Wpf.ViewModels.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.ViewModels
{
	public class PaginatedIntervalListViewModel : PaginatedItemListViewModel<IntervalListItemViewModel>
	{
		private readonly IIntervalRepository _intervalRepository;
		private readonly IIntervalListItemViewModelFactory _intervalListItemViewModelFactory;

		private readonly IntervalSearchFilter _intervalSearchFilter = new();

		private bool _matchingIntervalsLoaded;
		private IEnumerable<Interval> _allMatchingIntervals;

		public override IOrderedEnumerable<IntervalListItemViewModel> Items
			=> Page?.Items.OrderByDescending(listItem => listItem.Interval.Interval.From);

		public PaginatedIntervalListViewModel(
			IEventAggregator ea,
			IIntervalRepository intervalRepository,
			IIntervalListItemViewModelFactory intervalListItemViewModelFactory)
		{
			_intervalRepository = intervalRepository;
			_intervalListItemViewModelFactory = intervalListItemViewModelFactory;

			ea.RegisterHandler<ReportSourceChanged>(HandleReportSourceChanged);
			ea.RegisterHandler<IncludedActivitiesChanged>(HandleIncludedActivitiesChanged);
			ea.RegisterHandler<TimeSpanChanged>(HandleSearchBetweenDatesChanged);
		}

		protected override async Task<PaginatedList<IntervalListItemViewModel>> GetCurrentPageAsync()
		{
			await LoadUnloadedIntervals();

			PaginatedList<Interval> intervalPage = await _allMatchingIntervals.AsQueryable()
				.PaginatedListAsync(CurrentPagingParams);
			var y = intervalPage.TotalCount;
			PaginatedList<IntervalListItemViewModel> listItemPage = intervalPage.ToMappedPagedList(interval => MapIntervalToListItem(interval, intervalPage.Items));
			return listItemPage;
		}

		private IntervalListItemViewModel MapIntervalToListItem(Interval interval, IEnumerable<Interval> otherIntervals)
		{
			bool isLast = interval.IsLastOnDate(otherIntervals);
			return _intervalListItemViewModelFactory.Create(interval, isLast);
		}

		private async Task LoadUnloadedIntervals()
		{
			if (!_matchingIntervalsLoaded)
				await LoadAllIntervals();
		}
		private async Task LoadAllIntervals()
		{
			_allMatchingIntervals = await _intervalRepository.GetIntervals(_intervalSearchFilter.IncludedActivities,
				_intervalSearchFilter.From, _intervalSearchFilter.To);
			_matchingIntervalsLoaded = true;
		}

		private void HandleReportSourceChanged(ReportSourceChanged obj)
		{
			_matchingIntervalsLoaded = false;
			ResetToStartingPageCommand.Execute();
		}

		private void HandleIncludedActivitiesChanged(IncludedActivitiesChanged msg)
		{
			_intervalSearchFilter.IncludedActivities = msg.NewIncludedActivities;

			_matchingIntervalsLoaded = false;

			ResetToStartingPageCommand.Execute();
		}

		private void HandleSearchBetweenDatesChanged(TimeSpanChanged msg)
		{
			_intervalSearchFilter.From = msg.From;
			_intervalSearchFilter.To = msg.To;

			_matchingIntervalsLoaded = false;

			ResetToStartingPageCommand.Execute();
		}
	}
}
