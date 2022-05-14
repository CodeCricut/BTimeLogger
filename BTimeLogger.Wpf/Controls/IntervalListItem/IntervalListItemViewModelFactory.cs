using BTimeLogger.Wpf.Windows;

namespace BTimeLogger.Wpf.Controls;

public interface IIntervalListItemViewModelFactory
{
	IntervalListItemViewModel Create(Interval interval, bool isLastOnDate);
}

class IntervalListItemViewModelFactory : IIntervalListItemViewModelFactory
{
	private readonly IIntervalViewModelFactory _intervalViewModelFactory;
	private readonly IModifyIntervalWindowViewModelFactory _modifyIntervalWindowViewModelFactory;
	private readonly IViewManager _viewManager;

	public IntervalListItemViewModelFactory(IIntervalViewModelFactory intervalViewModelFactory,
		IModifyIntervalWindowViewModelFactory modifyIntervalWindowViewModelFactory,
		IViewManager viewManager)
	{
		_intervalViewModelFactory = intervalViewModelFactory;
		_modifyIntervalWindowViewModelFactory = modifyIntervalWindowViewModelFactory;
		_viewManager = viewManager;
	}

	public IntervalListItemViewModel Create(Interval interval, bool isLastOnDate)
	{
		IntervalViewModel intervalVM = _intervalViewModelFactory.Create(interval);
		return new IntervalListItemViewModel(intervalVM, isLastOnDate, _modifyIntervalWindowViewModelFactory, _viewManager);
	}
}