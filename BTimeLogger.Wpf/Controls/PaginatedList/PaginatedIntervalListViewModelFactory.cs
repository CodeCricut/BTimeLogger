using BTimeLogger.Domain.Services;

namespace BTimeLogger.Wpf.Controls;

public interface IPaginatedIntervalListViewModelFactory
{
	PaginatedIntervalListViewModel Create();
}
class PaginatedIntervalListViewModelFactory : IPaginatedIntervalListViewModelFactory
{
	private readonly IEventAggregator _ea;
	private readonly IIntervalRepository _intervalRepository;
	private readonly IIntervalListItemViewModelFactory _intervalListItemViewModelFactory;

	public PaginatedIntervalListViewModelFactory(IEventAggregator ea,
		IIntervalRepository intervalRepository,
		IIntervalListItemViewModelFactory intervalListItemViewModelFactory)
	{
		_ea = ea;
		_intervalRepository = intervalRepository;
		_intervalListItemViewModelFactory = intervalListItemViewModelFactory;
	}

	public PaginatedIntervalListViewModel Create()
	{
		return new PaginatedIntervalListViewModel(_ea, _intervalRepository, _intervalListItemViewModelFactory);
	}
}
