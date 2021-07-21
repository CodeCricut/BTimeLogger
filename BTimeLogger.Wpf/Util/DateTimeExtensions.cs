using System;

namespace BTimeLogger.Wpf.Util
{
	public static class DateTimeExtensions
	{
		public static string DateTimeString(this DateTime dt)
		{
			return dt.ToString("g");
		}
	}
}
