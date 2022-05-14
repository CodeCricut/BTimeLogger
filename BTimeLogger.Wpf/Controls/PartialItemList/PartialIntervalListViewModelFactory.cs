
namespace BTimeLogger.Wpf.Controls;

public interface IPartialIntervalListViewModelFactory
{
	PartialIntervalListViewModel Create(PaginatedIntervalListViewModel paginatedIntervalListViewModel);
}

class PartialIntervalListViewModelFactory : IPartialIntervalListViewModelFactory
{
	private readonly IEventAggregator _ea;

	public PartialIntervalListViewModelFactory(IEventAggregator ea)
	{
		_ea = ea;
	}

	public PartialIntervalListViewModel Create(PaginatedIntervalListViewModel paginatedIntervalListViewModel)
	{
		return new PartialIntervalListViewModel(_ea, paginatedIntervalListViewModel);
	}
}
