using BTimeLogger.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Services
{
	public interface IStatisticsGenerator
	{
		Task<Statistic> GenerateStatistic(Activity activity, DateTime from, DateTime to);
		Task<IEnumerable<Statistic>> GenerateStatistics(IEnumerable<Activity> statActivity, DateTime from, DateTime to);

		Task<Statistic> GenerateStatistic(Activity activity, TimeSpan totalTime, DateTime from, DateTime to);
		Task<IEnumerable<Statistic>> GenerateStatistics(IEnumerable<Activity> statActivity, TimeSpan totalTime, DateTime from, DateTime to);

		Task<IEnumerable<Statistic>> GenerateAllStatistics(DateTime from, DateTime to);
	}

	class StatisticsGenerator : IStatisticsGenerator
	{
		private static readonly byte TOTAL_PERCENT = 100;
		private readonly IIntervalRepository _intervalRepository;
		private readonly IActivityRepository _activityRepository;

		public StatisticsGenerator(IIntervalRepository intervalRepository,
			IActivityRepository activityRepository)
		{
			_intervalRepository = intervalRepository;
			_activityRepository = activityRepository;
		}

		public async Task<IEnumerable<Statistic>> GenerateStatistics(IEnumerable<Activity> activities, TimeSpan totalTime, DateTime from, DateTime to)
		{
			IEnumerable<Interval> intervalsBetweenDates = (await _intervalRepository.GetIntervals()).BetweenDates(from, to);

			List<Statistic> stats = new();
			foreach (var activity in activities)
			{
				IEnumerable<Interval> intervalsOfTypes = intervalsBetweenDates.OfActivityTypesOrAll(new ActivityCode[] { activity.Code }); // TODO GOD JUST CLEAN UP THIS ENTIRE CLASS :(
																																		   //IEnumerable<Interval> intervalsOfTypes = await _intervalRepository.GetIntervals(activity.Code, from, to);
				TimeSpan trackedDurationOfType = intervalsOfTypes.Duration();

				decimal percentOfTrackedTimeInTimespan = totalTime.TotalSeconds <= 1
					? 0M
					: (decimal)trackedDurationOfType.PercentOf(totalTime) * TOTAL_PERCENT;

				stats.Add(new Statistic()
				{
					Activity = activity,
					Duration = trackedDurationOfType,
					PercentOfTrackedTimeInTimespan = percentOfTrackedTimeInTimespan,
					From = from,
					To = to
				});
			}
			return stats;
		}

		public async Task<Statistic> GenerateStatistic(Activity activity, TimeSpan totalTime, DateTime from, DateTime to)
		{
			if (activity == null) throw new ArgumentNullException(nameof(activity));

			IEnumerable<Interval> intervalsOfTypes = await _intervalRepository.GetIntervals(activity.Code, from, to);
			TimeSpan trackedDurationOfType = intervalsOfTypes.Duration();

			decimal percentOfTrackedTimeInTimespan = totalTime.TotalSeconds <= 1
				? 0M
				: (decimal)trackedDurationOfType.PercentOf(totalTime) * TOTAL_PERCENT;

			return new Statistic()
			{
				Activity = activity,
				Duration = trackedDurationOfType,
				PercentOfTrackedTimeInTimespan = percentOfTrackedTimeInTimespan,
				From = from,
				To = to
			};
		}

		public async Task<Statistic> GenerateStatistic(Activity activity, DateTime from, DateTime to)
		{
			if (activity == null) throw new ArgumentNullException(nameof(activity));

			IEnumerable<Interval> intervalsBetweenDates = (await _intervalRepository.GetIntervals()).BetweenDates(from, to);
			TimeSpan totalTrackedDuration = intervalsBetweenDates.Duration();

			IEnumerable<Interval> intervalsOfTypes = intervalsBetweenDates.WithAncestorOfType(activity.Code);
			TimeSpan trackedDurationOfType = intervalsOfTypes.Duration();

			decimal percentOfTrackedTimeInTimespan = totalTrackedDuration.TotalSeconds <= 1
				? 0M
				: (decimal)trackedDurationOfType.PercentOf(totalTrackedDuration) * TOTAL_PERCENT;

			return new Statistic()
			{
				Activity = activity,
				Duration = trackedDurationOfType,
				PercentOfTrackedTimeInTimespan = percentOfTrackedTimeInTimespan,
				From = from,
				To = to
			};
		}


		public async Task<IEnumerable<Statistic>> GenerateStatistics(IEnumerable<Activity> statActivities, DateTime from, DateTime to)
		{
			IEnumerable<Interval> intervalsBetweenDates = (await _intervalRepository.GetIntervals()).BetweenDates(from, to);
			TimeSpan totalTrackedDuration = intervalsBetweenDates.Duration();

			List<Statistic> stats = new();
			foreach (var activity in statActivities)
			{
				IEnumerable<Interval> intervalsOfTypes = intervalsBetweenDates.WithAncestorOfType(activity.Code);
				TimeSpan trackedDurationOfType = intervalsOfTypes.Duration();

				decimal percentOfTrackedTimeInTimespan = totalTrackedDuration.TotalSeconds <= 1
					? 0M
					: (decimal)trackedDurationOfType.PercentOf(totalTrackedDuration) * TOTAL_PERCENT;

				Statistic stat = new Statistic()
				{
					Activity = activity,
					Duration = trackedDurationOfType,
					PercentOfTrackedTimeInTimespan = percentOfTrackedTimeInTimespan,
					From = from,
					To = to
				};
				stats.Add(stat);
			}

			return stats;

		}

		public async Task<IEnumerable<Statistic>> GenerateAllStatistics(DateTime from, DateTime to)
		{
			IEnumerable<Activity> allActivityTypes = await _activityRepository.GetActivities();
			return await GenerateStatistics(allActivityTypes, from, to);
		}
	}
}
