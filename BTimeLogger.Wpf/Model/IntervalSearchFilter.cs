using System;

namespace BTimeLogger.Wpf.Model
{
	public class IntervalSearchFilter
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public Activity[] IncludedActivities { get; set; } = Array.Empty<Activity>();
	}
}
