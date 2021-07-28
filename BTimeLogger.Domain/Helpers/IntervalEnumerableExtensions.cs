using System;
using System.Collections.Generic;
using System.Linq;

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
			IEnumerable<Interval> filteredIntervals = intervals.ToArray();
			if (fromDate.HasValue)
				filteredIntervals = filteredIntervals.From(fromDate.Value, useOnlyDate);
			if (toDate.HasValue)
				filteredIntervals = filteredIntervals.To(toDate.Value, useOnlyDate);
			return filteredIntervals;
		}

		public static IEnumerable<Interval> OfActivityType(this IEnumerable<Interval> intervals, Activity activityType, IEqualityComparer<Activity> activityComparer = null)
		{
			if (activityComparer == null) activityComparer = new ActivityNameEqualityOperator();
			return intervals.Where(interval => activityComparer.Equals(interval.Activity, activityType));
		}

		public static IEnumerable<Interval> OfActivityTypes(this IEnumerable<Interval> intervals, Activity[] activityTypes, IEqualityComparer<Activity> activityComparer = null)
		{
			if (activityComparer == null) activityComparer = new ActivityNameEqualityOperator();
			return intervals.Where(interval =>
			{
				Activity activity = interval.Activity;
				if (activityTypes.Contains(interval.Activity, activityComparer)) return true;

				Activity currentGroupAncestor = activity.Parent;
				while (currentGroupAncestor != null)
				{
					if (activityTypes.Contains(currentGroupAncestor, activityComparer)) return true;
					currentGroupAncestor = currentGroupAncestor.Parent;
				}
				return false;
			});
		}


		public static IEnumerable<Interval> OfActivityTypesOrAll(this IEnumerable<Interval> intervals, Activity[] activityTypes, IEqualityComparer<Activity> activityComparer = null)
		{
			if (activityTypes?.Length <= 0) return intervals;
			return intervals.OfActivityTypes(activityTypes, activityComparer);
		}
	}
}
