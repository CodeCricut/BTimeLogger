using System;

namespace BTimeLogger
{
	public class Statistic
	{
		public Activity Activity { get; set; }
		public TimeSpan Duration { get; set; }
		public decimal PercentOfTrackedTimeInTimespan { get; set; }

		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
