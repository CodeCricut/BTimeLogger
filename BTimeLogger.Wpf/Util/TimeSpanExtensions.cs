using System;

namespace BTimeLogger.Wpf.Util
{
	public static class TimeSpanExtensions
	{
		public static string DurationString(this TimeSpan ts)
		{
			return ts.Duration().ToString("HH:mm");
		}
	}
}
