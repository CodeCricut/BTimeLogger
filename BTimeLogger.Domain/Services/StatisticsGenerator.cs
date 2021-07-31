using BTimeLogger.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BTimeLogger.Domain.Services
{
	public interface IStatisticsGenerator
	{
		Task<Statistic> GenerateStatistic(Activity activity, DateTime from, DateTime to);
		Task<IEnumerable<Statistic>> GenerateStatistics(IEnumerable<Activity> statActivity, DateTime from, DateTime to);
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
			List<Task<Statistic>> generateStatTasks = new();
			foreach (var statActivity in statActivities)
				generateStatTasks.Add(GenerateStatistic(statActivity, from, to));

			return await Task.WhenAll(generateStatTasks);
		}

		public async Task<IEnumerable<Statistic>> GenerateAllStatistics(DateTime from, DateTime to)
		{
			IEnumerable<Activity> allActivityTypes = await _activityRepository.GetActivities();
			return await GenerateStatistics(allActivityTypes, from, to);
		}
	}
}
