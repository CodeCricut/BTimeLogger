using System;
using System.Collections.Generic;
using System.Linq;

namespace BTimeLogger.Domain.Helpers
{
	public static class IntervalEnumerableExtensions
	{
		/// <summary>
		/// Get all intervals which begin or end after the <paramref name="fromDate"/> (inclusively).
		/// </summary>
		/// <param name="useOnlyDate">Don't consider the time in the comparison of interval date-times.</param>
		public static IEnumerable<Interval> From(this IEnumerable<Interval> intervals, DateTime fromDate, bool useOnlyDate = true)
		{
			if (useOnlyDate)
				return intervals.Where(interval => interval.To.Date >= fromDate.Date);
			else
				return intervals.Where(intervals => intervals.To >= fromDate);
		}

		/// <summary>
		/// Get all intervals which begin or end before the <paramref name="toDate"/> (inclusively).
		/// </summary>
		/// <param name="useOnlyDate">Don't consider the time in the comparison of interval date-times.</param>
		public static IEnumerable<Interval> To(this IEnumerable<Interval> intervals, DateTime toDate, bool useOnlyDate = true)
		{
			if (useOnlyDate)
				return intervals.Where(interval => interval.From.Date <= toDate.Date);
			else
				return intervals.Where(intervals => intervals.From <= toDate);
		}

		/// <summary>
		/// Get all intervals which begin or end within the time window (inclusive of time bounds).
		/// </summary>
		/// <param name="useOnlyDate">Don't consider the time in the comparison of interval date-times.</param>
		public static IEnumerable<Interval> BetweenDates(this IEnumerable<Interval> intervals, DateTime fromDate, DateTime toDate, bool useOnlyDate = true)
		{
			return intervals.From(fromDate, useOnlyDate).To(toDate, useOnlyDate);
		}


		/// <summary>
		///  Get all intervals which begin or end within the time window (inclusive of time bounds).
		/// </summary>
		/// <param name="intervals"></param>
		/// <param name="fromDate">If a value is present, only consider intervals which start or end after this time.</param>
		/// <param name="toDate">If a value is present, only consider intervals which start or end before this time.</param>
		/// <param name="useOnlyDate">Don't consider the time in the comparison of interval date-times.</param>
		/// <returns></returns>
		public static IEnumerable<Interval> BetweenDates(this IEnumerable<Interval> intervals, DateTime? fromDate, DateTime? toDate, bool useOnlyDate = true)
		{
			IEnumerable<Interval> filteredIntervals = intervals;
			if (fromDate.HasValue)
				filteredIntervals = filteredIntervals.From(fromDate.Value, useOnlyDate);
			if (toDate.HasValue)
				filteredIntervals = filteredIntervals.To(toDate.Value, useOnlyDate);
			return filteredIntervals;
		}


		/// <summary>
		/// Get all <see cref="Interval"/>s in <paramref name="intervals"/> which have an ancestor of the activity type.
		/// The activity type ancestors of an interval include the activity type of the interval and the activity types of 
		/// all parents of that type (recursively).
		/// </summary>
		public static IEnumerable<Interval> WithAncestorOfType(this IEnumerable<Interval> intervals, ActivityCode activityType)
		{
			if (intervals == null) throw new ArgumentNullException(nameof(intervals));
			if (activityType == null) throw new ArgumentNullException(nameof(activityType));

			return intervals.Where(interval =>
			{
				ActivityCode intervalActivityType = interval.Activity.Code;
				return intervalActivityType.AncestorCodes.Contains(activityType);
			});
		}

		/// <summary>
		/// Return the <paramref name="intervals"/> whose activity types are included in <paramref name="activityTypes"/>.
		/// </summary>
		public static IEnumerable<Interval> OfActivityTypes(this IEnumerable<Interval> intervals, IEnumerable<ActivityCode> activityTypes)
		{
			return intervals.Where(interval =>
			{
				IEnumerable<ActivityCode> ancestorCodes = interval.Activity.Code.AncestorCodes;
				bool hasAncestorOfProvidedType = ancestorCodes.Any(code => activityTypes.Contains(code));
				return hasAncestorOfProvidedType;
			});
		}

		/// <summary>
		/// If no <paramref name="activityTypes"/> are given, return <paramref name="intervals"/> unfiltered. Otherwise,
		/// return the intervals which activity types are included in <paramref name="activityTypes"/>.
		/// </summary>
		public static IEnumerable<Interval> OfActivityTypesOrAll(this IEnumerable<Interval> intervals, IEnumerable<ActivityCode> activityTypes)
		{
			if (activityTypes?.Count() <= 0) return intervals;
			return intervals.OfActivityTypes(activityTypes);
		}

		/// <summary>
		/// Get the total duration of all of the <paramref name="intervals"/>, which is the aggregate of <see cref="Interval.Duration"/>.
		/// </summary>
		public static TimeSpan Duration(this IEnumerable<Interval> intervals)
		{
			if (intervals.Count() <= 0) return TimeSpan.Zero;
			return intervals.Select(interval => interval.Duration).Aggregate((dur1, dur2) => dur1.Add(dur2));
		}

		/// <summary>
		/// Get the total duration of all of the <paramref name="stats"/>, which is the aggregate of <see cref="Statistic.Duration"/>.
		/// </summary>
		public static TimeSpan Duration(this IEnumerable<Statistic> stats)
		{
			if (stats.Count() <= 0) return TimeSpan.Zero;
			return stats.Select(stat => stat.Duration).Aggregate((dur1, dur2) => dur1.Add(dur2));
		}
	}
}
