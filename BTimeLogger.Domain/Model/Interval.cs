using System;

namespace BTimeLogger
{
	/// <summary>
	/// Represents the time spent doing a certain <see cref="Activity"/> and the data that pertains to tracking an activity.
	/// </summary>
	public class Interval
	{
		public Activity Activity { get; set; }
		public TimeSpan Duration { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public string Comment { get; set; }

		public Guid Guid { get; set; }
	}
}
