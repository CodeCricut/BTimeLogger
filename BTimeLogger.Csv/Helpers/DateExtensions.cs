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
			if (duration.TotalDays >= 1)
				return $"{duration.TotalDays.ToString("N0")} days, {duration.ToString("hh':'mm")}";
			return duration.ToString("hh':'mm");
		}
	}
}
