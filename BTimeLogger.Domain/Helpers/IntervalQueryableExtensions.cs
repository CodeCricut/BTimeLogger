using System;
using System.Linq;

namespace BTimeLogger.Domain;

public static class IntervalQueryableExtensions
{
	public static IQueryable<Interval> BetweenDates(this IQueryable<Interval> intervals, DateTime from, DateTime to)
	{
		return intervals.Where(interval => interval.To >= from && interval.From <= to);
	}
}
