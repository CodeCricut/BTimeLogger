using BTimeLogger.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Controls;

/// <summary>
/// Represents a page of entities and the logic related to loading and displaying that page.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PaginatedItemListViewModel<T> : BaseViewModel
{
	#region VM Props 
	public PagingParams CurrentPagingParams { get; private set; } = new PagingParams();

	private PaginatedList<T> _page;
	public PaginatedList<T> Page
	{
		get => _page;
		set
		{
			Set(ref _page, value, ALL_PROPS_CHANGED);
			NextPageCommand.RaiseCanExecuteChanged();
			PrevPageCommand.RaiseCanExecuteChanged();
		}
	}

	public abstract IOrderedEnumerable<T> Items { get; }

	public int CurrentPage => Page?.PageIndex ?? 0;
	public int TotalPages => Page?.TotalPages ?? 0;

	public int TotalCount => Page?.TotalCount ?? 0;

	public int PageSize => Page?.PageSize ?? 0;

	public bool HasPrevPage => Page?.HasPreviousPage ?? false;
	public PagingParams PrevPagingParams => Page?.PreviousPagingParams;

	public bool HasNextPage => Page?.HasNextPage ?? false;
	public PagingParams NextPagingParams => Page?.NextPagingParams;

	private bool _loading;
	public bool Loading
	{
		get => _loading;
		set
		{
			Set(ref _loading, value);
			RaisePropertyChanged(nameof(IsEmpty));
		}
	}

	public bool IsEmpty { get => TotalCount <= 0 && !Loading; }

	#endregion

	public AsyncDelegateCommand LoadCurrentPageCommand { get; }
	public AsyncDelegateCommand PrevPageCommand { get; }
	public AsyncDelegateCommand NextPageCommand { get; }
	public AsyncDelegateCommand ResetToStartingPageCommand { get; }

	public PaginatedItemListViewModel()
	{
		LoadCurrentPageCommand = new AsyncDelegateCommand(LoadCurrentPage);
		PrevPageCommand = new AsyncDelegateCommand(LoadPrevPage, _ => HasPrevPage);
		NextPageCommand = new AsyncDelegateCommand(LoadNextPage, _ => HasNextPage);
		ResetToStartingPageCommand = new AsyncDelegateCommand(ResetToStartingPage);
	}

	public async Task LoadNextPage(object parameter = null)
	{
		IncrementPagingParams();

		await LoadCurrentPage();
	}

	public async Task LoadPrevPage(object parameter = null)
	{
		DecrementPagingParams();

		await LoadCurrentPage();
	}

	public async Task LoadCurrentPage(object param = null)
	{
		Page = new PaginatedList<T>(new(), TotalCount, CurrentPage, PageSize);

		Loading = true;
		PaginatedList<T> currentPage = await Task.Run(GetCurrentPageAsync);
		Loading = false;

		Page = currentPage;
	}

	public async Task ResetToStartingPage(object param = null)
	{
		ResetPagingParams();
		await LoadCurrentPage();
	}

	protected abstract Task<PaginatedList<T>> GetCurrentPageAsync();

	public void IncrementPagingParams()
	{
		CurrentPagingParams = NextPagingParams;
	}

	public void DecrementPagingParams()
	{
		CurrentPagingParams = PrevPagingParams;
	}

	public void ResetPagingParams()
	{
		CurrentPagingParams = new PagingParams();
	}
}
