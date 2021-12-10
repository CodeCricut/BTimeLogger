using System;
using System.Collections.Generic;

namespace BTimeLogger.Domain
{
	/// <summary>
	///  Statistical data about time spent doing activites which are found within the activity group. 
	///  A parent of <see cref="Statistic"/> objects for each child activity in the activity group.
	/// </summary>
	public class GroupStatistic
	{
		public ActivityCode ActivityType { get; set; }
		public TimeSpan Duration { get; set; }
		public IEnumerable<Statistic> Children { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
