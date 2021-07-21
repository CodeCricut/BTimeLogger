using System.Collections.Generic;
using System.Linq;

namespace BTimeLogger.Wpf.Util
{
	public static class IntervalExtensions
	{
		public static bool IsLastOnDate(this Interval interval, IEnumerable<Interval> otherIntervals)
		{
			var intervalsOnDate = otherIntervals.Where(otherInterval => otherInterval.From.Date == interval.From.Date);
			var lastIntervalOnDate = intervalsOnDate.OrderByDescending(intervalOnDate => intervalOnDate.From).First();
			bool isLastOnDate = interval.Equals(lastIntervalOnDate);
			return isLastOnDate;
		}
	}
}
