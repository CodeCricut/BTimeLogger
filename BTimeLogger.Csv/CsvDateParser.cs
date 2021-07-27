using System;

namespace BTimeLogger.Csv
{
	public static class CsvDateParser
	{
		public static readonly string CSV_FORMAT_WITH_SECONDS = "yyyy-MM-dd HH:mm:ss";
		public static readonly string CSV_FORMAT_WITHOUT_SECONDS = "yyyy-MM-dd HH:mm";

		public static DateTime Parse(string dateStr)
		{
			return DateTime.Parse(dateStr);
		}
	}
}
