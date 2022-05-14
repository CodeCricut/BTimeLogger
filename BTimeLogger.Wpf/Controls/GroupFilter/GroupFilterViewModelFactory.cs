using MediatR;

namespace BTimeLogger.Wpf.Controls;

public interface IGroupFilterViewModelFactory
{
	GroupFilterViewModel Create();
}
class GroupFilterViewModelFactory : IGroupFilterViewModelFactory
{
	private readonly IEventAggregator _ea;
	private readonly IMediator _mediator;

	public GroupFilterViewModelFactory(IEventAggregator ea,
		IMediator mediator)
	{
		_ea = ea;
		_mediator = mediator;
	}
	public GroupFilterViewModel Create()
	{
		return new GroupFilterViewModel(_ea, _mediator);
	}
}
