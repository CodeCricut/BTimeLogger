using BTimeLogger.Wpf.ViewModels.Domain;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IIntervalListItemViewModelFactory
	{
		IntervalListItemViewModel Create(Interval interval, bool isLastOnDate);
	}

	class IntervalListItemViewModelFactory : IIntervalListItemViewModelFactory
	{
		private readonly IIntervalViewModelFactory _intervalViewModelFactory;

		public IntervalListItemViewModelFactory(IIntervalViewModelFactory intervalViewModelFactory)
		{
			_intervalViewModelFactory = intervalViewModelFactory;
		}

		public IntervalListItemViewModel Create(Interval interval, bool isLastOnDate)
		{
			IntervalViewModel intervalVM = _intervalViewModelFactory.Create(interval);
			return new IntervalListItemViewModel(intervalVM, isLastOnDate);
		}
	}
}