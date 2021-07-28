using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
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
}
