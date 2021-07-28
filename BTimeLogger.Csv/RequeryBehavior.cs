using System;

namespace BTimeLogger.Csv
{
	public abstract class RequeryBehavior
	{
		public abstract bool ShouldRequery { get; }
	}

	public class TimedRequeryBehavior : RequeryBehavior
	{
		private readonly TimeSpan _timeBetweenRequeries;

		public DateTime LastRequeried { get; private set; }

		public override bool ShouldRequery => LastRequeried + _timeBetweenRequeries <= DateTime.Now;

		public TimedRequeryBehavior(TimeSpan timeBetweenRequeries)
		{
			_timeBetweenRequeries = timeBetweenRequeries;
		}

		public void Requery()
		{
			LastRequeried = DateTime.Now;
		}
	}
}
