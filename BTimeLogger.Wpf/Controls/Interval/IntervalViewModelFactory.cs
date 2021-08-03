namespace BTimeLogger.Wpf.Controls
{
	public interface IIntervalViewModelFactory
	{
		IntervalViewModel Create(Interval interval);
	}

	class IntervalViewModelFactory : IIntervalViewModelFactory
	{
		private readonly IActivityViewModelFactory _activityViewModelFactory;

		public IntervalViewModelFactory(IActivityViewModelFactory activityViewModelFactory)
		{
			_activityViewModelFactory = activityViewModelFactory;
		}

		public IntervalViewModel Create(Interval interval)
		{
			ActivityViewModel activityVM = _activityViewModelFactory.Create(interval.Activity);
			return new(interval, activityVM);
		}
	}
}
