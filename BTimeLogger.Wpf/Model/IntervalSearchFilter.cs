using System;
using System.Collections.Generic;
using static BTimeLogger.Activity;

namespace BTimeLogger.Wpf.Model
{
	public class IntervalSearchFilter
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public IEnumerable<ActivityCode> IncludedActivities { get; set; } = Array.Empty<ActivityCode>();
	}
}
