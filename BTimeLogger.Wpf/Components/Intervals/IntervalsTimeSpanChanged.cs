using System;

namespace BTimeLogger.Wpf.Components.Intervals
{
	class IntervalsTimeSpanChanged
	{
		public IntervalsTimeSpanChanged(DateTime from, DateTime to)
		{
			From = from;
			To = to;
		}

		public DateTime From { get; }
		public DateTime To { get; }
	}
}
