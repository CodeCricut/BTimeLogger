using BTimeLogger.Domain.Services;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Controls
{
	public interface IIntervalListViewModelFactory
	{
		IntervalListViewModel Create();
	}

	class IntervalListViewModelFactory : IIntervalListViewModelFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IIntervalRepository _intervalRepository;
		private readonly IIntervalListItemViewModelFactory _intervalListItemVMFactory;

		public IntervalListViewModelFactory(IEventAggregator ea,
			IIntervalRepository intervalRepository,
			IIntervalListItemViewModelFactory intervalListItemVMFactory
			)
		{
			_ea = ea;
			_intervalRepository = intervalRepository;
			_intervalListItemVMFactory = intervalListItemVMFactory;
		}

		public IntervalListViewModel Create()
		{
			return new IntervalListViewModel(_ea, _intervalRepository, _intervalListItemVMFactory);
		}
	}
}
