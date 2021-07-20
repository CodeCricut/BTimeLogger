using System;

namespace BTimeLogger.Wpf.Util
{
	public static class DateTimeExtensions
	{
		public static string DateString(this DateTime dt)
		{
			return dt.ToString("g");
		}
	}
}
