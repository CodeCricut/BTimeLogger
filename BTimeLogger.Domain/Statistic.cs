using System;

namespace BTimeLogger
{
	public class Statistic
	{
		public int Id { get; set; }
		public Activity Activity { get; set; }
		public TimeSpan Duration { get; set; }
		public decimal Percent { get; set; }
	}
}
