using System;

namespace BTimeLogger.Wpf.Controls;

public class IntervalsTimeSpanChanged
{
	public IntervalsTimeSpanChanged(DateTime from, DateTime to)
	{
		From = from;
		To = to;
	}

	public DateTime From { get; }
	public DateTime To { get; }
}
