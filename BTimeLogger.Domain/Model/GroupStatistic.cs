using System;
using System.Collections.Generic;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain
{
	public class GroupStatistic
	{
		public ActivityCode ActivityType { get; set; }
		public TimeSpan Duration { get; set; }
		public IEnumerable<Statistic> Children { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
