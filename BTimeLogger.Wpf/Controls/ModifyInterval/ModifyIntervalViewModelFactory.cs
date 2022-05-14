using MediatR;

namespace BTimeLogger.Wpf.Controls;

public interface IModifyIntervalViewModelFactory
{
	ModifyIntervalViewModel Create(IntervalViewModel intervalViewModel,
		ActivityTypeSelectorViewModel activityTypeSelectorViewModel);
}
class ModifyIntervalViewModelFactory : IModifyIntervalViewModelFactory
{
	private readonly IMediator _mediator;

	public ModifyIntervalViewModelFactory(IMediator mediator)
	{
		_mediator = mediator;
	}

	public ModifyIntervalViewModel Create(IntervalViewModel intervalViewModel,
		ActivityTypeSelectorViewModel activityTypeSelectorViewModel)
	{
		return new ModifyIntervalViewModel(intervalViewModel, activityTypeSelectorViewModel, _mediator);
	}
}
