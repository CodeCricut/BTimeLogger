using System;

namespace BTimeLogger.Csv.Helpers
{
	public static class CsvFieldParser
	{
		private static readonly char DURATION_DELIM = ':';

		public static DateTime ParseDate(string dateStr)
		{
			return DateTime.Parse(dateStr);
		}

		public static TimeSpan ParseDuration(string duration)
		{
			string[] parts = duration.Split(DURATION_DELIM);
			if (parts.Length == 3)
				return ParseHourMinSec(parts);
			else if (parts.Length == 2)
				return ParseHoursMin(parts);
			else throw new ArgumentOutOfRangeException(nameof(duration));
		}

		private static TimeSpan ParseHoursMin(string[] parts)
		{
			int hours = int.Parse(parts[0]);
			int mins = int.Parse(parts[1]);
			return new TimeSpan(hours, mins, 0);
		}

		private static TimeSpan ParseHourMinSec(string[] parts)
		{
			int hours = int.Parse(parts[0]);
			int mins = int.Parse(parts[1]);
			int seconds = int.Parse(parts[2]);
			return new TimeSpan(hours, mins, seconds);
		}

		public static decimal ParsePercent(string percent)
		{
			return decimal.Parse(percent);
		}
	}
}
