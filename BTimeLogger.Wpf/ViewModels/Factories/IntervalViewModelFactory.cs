using BTimeLogger.Wpf.ViewModels.Domain;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IIntervalViewModelFactory
	{
		IntervalViewModel Create(Interval interval);
	}

	class IntervalViewModelFactory : IIntervalViewModelFactory
	{
		public IntervalViewModel Create(Interval interval)
		{
			return new(interval);
		}
	}
}
