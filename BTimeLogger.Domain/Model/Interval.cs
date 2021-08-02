using System;

namespace BTimeLogger
{
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
