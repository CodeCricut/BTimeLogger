using System;

namespace BTimeLogger.Csv.Helpers
{
	public static class DateExtensions
	{
		public static string ToCsvFormat(this DateTime date)
		{
			return date.ToString("yyyy-MM-dd HH:mm");
		}
	}

	public static class TimeSpanExtensions
	{
		public static string ToCsvFormat(this TimeSpan duration)
		{
			return duration.ToString("hh':'mm");
		}
	}
}
