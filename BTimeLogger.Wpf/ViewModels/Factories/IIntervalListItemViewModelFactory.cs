using BTimeLogger.Wpf.ViewModels.Domain;

namespace BTimeLogger.Wpf.ViewModels
{
	public interface IIntervalListItemViewModelFactory
	{
		IntervalListItemViewModel Create(Interval interval);
	}

	class IntervalListItemViewModelFactory : IIntervalListItemViewModelFactory
	{
		public IntervalListItemViewModel Create(Interval interval)
		{
			return new IntervalListItemViewModel(interval);
		}
	}
}