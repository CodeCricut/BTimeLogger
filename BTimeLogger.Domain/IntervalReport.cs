using System;

namespace BTimeLogger
{
	public class IntervalReport
	{
		public TimeSpan TrackedDuration { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public Interval[] Items { get; set; }
		public Activity[] IncludedActivities { get; set; }
	}
}
