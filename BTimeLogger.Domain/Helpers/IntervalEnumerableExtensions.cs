using System;
using System.Collections.Generic;
using System.Linq;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Helpers
{
	public static class IntervalEnumerableExtensions
	{
		public static IEnumerable<Interval> From(this IEnumerable<Interval> intervals, DateTime fromDate, bool useOnlyDate = true)
		{
			if (useOnlyDate)
				return intervals.Where(interval => interval.To.Date >= fromDate.Date);
			else
				return intervals.Where(intervals => intervals.To >= fromDate);
		}

		public static IEnumerable<Interval> To(this IEnumerable<Interval> intervals, DateTime toDate, bool useOnlyDate = true)
		{
			if (useOnlyDate)
				return intervals.Where(interval => interval.From.Date <= toDate.Date);
			else
				return intervals.Where(intervals => intervals.From <= toDate);
		}

		public static IEnumerable<Interval> BetweenDates(this IEnumerable<Interval> intervals, DateTime fromDate, DateTime toDate, bool useOnlyDate = true)
		{
			return intervals.From(fromDate, useOnlyDate).To(toDate, useOnlyDate);
		}

		public static IEnumerable<Interval> BetweenDates(this IEnumerable<Interval> intervals, DateTime? fromDate, DateTime? toDate, bool useOnlyDate = true)
		{
			IEnumerable<Interval> filteredIntervals = intervals;
			if (fromDate.HasValue)
				filteredIntervals = filteredIntervals.From(fromDate.Value, useOnlyDate);
			if (toDate.HasValue)
				filteredIntervals = filteredIntervals.To(toDate.Value, useOnlyDate);
			return filteredIntervals;
		}

		public static IEnumerable<Interval> WithAncestorOfType(this IEnumerable<Interval> intervals, ActivityCode activityType)
		{
			if (intervals == null) throw new ArgumentNullException(nameof(intervals));
			if (activityType == null) throw new ArgumentNullException(nameof(activityType));

			return intervals.Where(interval =>
			{
				ActivityCode intervalActivityType = interval.Activity.Code;
				bool hasAncestorOfType = intervalActivityType.AncestorCodes.Contains(activityType);
				return hasAncestorOfType;
			});
		}

		public static IEnumerable<Interval> OfActivityTypes(this IEnumerable<Interval> intervals, IEnumerable<ActivityCode> activityTypes)
		{
			return intervals.Where(interval =>
			{
				IEnumerable<ActivityCode> ancestorCodes = interval.Activity.Code.AncestorCodes;
				bool hasAncestorOfProvidedType = ancestorCodes.Any(code => activityTypes.Contains(code));
				return hasAncestorOfProvidedType;
			});
		}

		public static IEnumerable<Interval> OfActivityTypesOrAll(this IEnumerable<Interval> intervals, IEnumerable<ActivityCode> activityTypes)
		{
			if (activityTypes?.Count() <= 0) return intervals;
			return intervals.OfActivityTypes(activityTypes);
		}

		public static TimeSpan Duration(this IEnumerable<Interval> intervals)
		{
			if (intervals.Count() <= 0) return TimeSpan.Zero;
			return intervals.Select(interval => interval.Duration).Aggregate((dur1, dur2) => dur1.Add(dur2));
		}

		public static TimeSpan Duration(this IEnumerable<Statistic> stats)
		{
			if (stats.Count() <= 0) return TimeSpan.Zero;
			return stats.Select(stat => stat.Duration).Aggregate((dur1, dur2) => dur1.Add(dur2));
		}
	}
}
