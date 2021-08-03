using MediatR;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IGroupedActivityFilterViewModelFactory
	{
		GroupedActivityFilterViewModel Create();
	}

	class GroupedActivityFilterViewModelFactory : IGroupedActivityFilterViewModelFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IMediator _mediator;

		public GroupedActivityFilterViewModelFactory(IEventAggregator ea,
			 IMediator mediator)
		{
			_ea = ea;
			_mediator = mediator;
		}

		public GroupedActivityFilterViewModel Create()
		{
			return new GroupedActivityFilterViewModel(_ea, _mediator);
		}
	}
}
