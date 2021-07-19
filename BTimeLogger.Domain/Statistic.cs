using System;

namespace BTimeLogger
{
	public class Statistic
	{
		public Activity Activity { get; set; }
		public TimeSpan Duration { get; set; }
		public decimal Percent { get; set; }
	}
}
