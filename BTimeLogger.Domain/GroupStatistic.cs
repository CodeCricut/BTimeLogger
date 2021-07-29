using System;
using System.Collections.Generic;

namespace BTimeLogger.Domain
{
	public class GroupStatistic
	{
		public Activity ActivityType { get; set; }
		public TimeSpan Duration { get; set; }
		public decimal PercentTotal { get; set; }
		public decimal PercentParent { get; set; }
		public IEnumerable<GroupStatistic> Children { get; set; }
	}
}
