using System;
using System.Diagnostics;

namespace BTimeLogger.Domain.Helpers
{
	public static class TimerUtil
	{
		public static TimeSpan Time(Action action)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			action();
			stopwatch.Stop();
			return stopwatch.Elapsed;
		}
	}
}
