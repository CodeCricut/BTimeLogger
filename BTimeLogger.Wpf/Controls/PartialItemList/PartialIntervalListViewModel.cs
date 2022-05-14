using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Controls;

public class PartialIntervalListViewModel : PartialItemListViewModel<IntervalListItemViewModel>
{
	private readonly PaginatedIntervalListViewModel _paginatedIntervalListViewModel;

	public override int TotalItems => _paginatedIntervalListViewModel.TotalCount;

	public override bool HasMoreItems => _paginatedIntervalListViewModel.Page?.HasNextPage ?? true;

	public bool ShowExplicitLoadMore { get => TotalLoaded <= 10; }

	public PartialIntervalListViewModel(
		IEventAggregator ea,
		PaginatedIntervalListViewModel paginatedIntervalListViewModel)
	{
		_paginatedIntervalListViewModel = paginatedIntervalListViewModel
			?? throw new ArgumentNullException(nameof(paginatedIntervalListViewModel));

		ea.RegisterHandler<ReportSourceChanged>(msg => CriteriaChanged());
		ea.RegisterHandler<IncludedIntervalActivitiesChanged>(msg => CriteriaChanged());
		ea.RegisterHandler<IntervalsTimeSpanChanged>(msg => CriteriaChanged());
	}

	protected override async Task<IEnumerable<IntervalListItemViewModel>> GetCurrentItemsAsync()
	{
		await _paginatedIntervalListViewModel.LoadCurrentPage();
		IEnumerable<IntervalListItemViewModel> currentItems = _paginatedIntervalListViewModel.Items.AsEnumerable();

		_paginatedIntervalListViewModel.IncrementPagingParams();

		return currentItems;
	}

	protected override Task ResetToStartingItemsAsync()
	{
		return _paginatedIntervalListViewModel.ResetToStartingPage();
	}

	private void CriteriaChanged()
	{
		if (!Loading)
			ResetItemsCommand.Execute();
	}
}
