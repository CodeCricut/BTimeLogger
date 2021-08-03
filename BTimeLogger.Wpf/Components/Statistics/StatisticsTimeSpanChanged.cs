using System;

namespace BTimeLogger.Wpf.Components.Statistics
{
	class StatisticsTimeSpanChanged
	{
		public StatisticsTimeSpanChanged(DateTime from, DateTime to)
		{
			From = from;
			To = to;
		}

		public DateTime From { get; }
		public DateTime To { get; }
	}
}
