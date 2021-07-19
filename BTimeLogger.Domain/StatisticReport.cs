using System;

namespace BTimeLogger
{
	public class StatisticsReport
	{
		public TimeSpan TrackedDuration { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public Statistic[] Items { get; set; }
		public Activity[] IncludedActivities { get; set; }
	}
}
