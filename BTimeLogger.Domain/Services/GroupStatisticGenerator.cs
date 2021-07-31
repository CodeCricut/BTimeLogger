using BTimeLogger.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Services
{
	public interface IGroupStatisticGenerator
	{
		Task<GroupStatistic> GenerateForGroup(Activity group, DateTime from, DateTime to);
		Task<GroupStatistic> GenerateGroupOfParentlessStatistics(DateTime from, DateTime to);
	}

	class GroupStatisticGenerator : IGroupStatisticGenerator
	{
		private readonly IStatisticsGenerator _statisticsGenerator;
		private readonly IActivityRepository _activityRepository;
		private readonly IIntervalRepository _intervalRepository;

		public GroupStatisticGenerator(IStatisticsGenerator statisticsGenerator, IActivityRepository activityRepository,
			IIntervalRepository intervalRepository)
		{
			_statisticsGenerator = statisticsGenerator;
			_activityRepository = activityRepository;
			_intervalRepository = intervalRepository;
		}

		public async Task<GroupStatistic> GenerateGroupOfParentlessStatistics(DateTime from, DateTime to)
		{
			IEnumerable<Activity> parentlessActivites = (await _activityRepository.GetActivities()).Where(act => !act.HasParent);

			//IEnumerable<Statistic> allStats = await _statisticsGenerator.GenerateAllStatistics(from, to);
			IEnumerable<Statistic> parentlessStats = await _statisticsGenerator.GenerateStatistics(parentlessActivites, from, to);

			TimeSpan totalTrackedTime = (await _intervalRepository.GetIntervals()).Duration();
			//allStats.Duration();			


			return new GroupStatistic()
			{
				ActivityType = ActivityCode.CreateCode("Total", Array.Empty<string>()),
				From = from,
				To = to,
				Duration = totalTrackedTime,
				Children = parentlessStats
			};
		}

		public async Task<GroupStatistic> GenerateForGroup(Activity group, DateTime from, DateTime to)
		{
			if (group == null)
				return await GenerateGroupOfParentlessStatistics(from, to);

			Statistic groupStat = await _statisticsGenerator.GenerateStatistic(group, from, to);
			TimeSpan totalTime = groupStat.Duration;

			IEnumerable<Statistic> childrenStats = await _statisticsGenerator.GenerateStatistics(group.Children, totalTime, from, to);

			return new GroupStatistic()
			{
				ActivityType = group.Code,
				From = from,
				To = to,
				Duration = groupStat.Duration,
				Children = childrenStats
			};
		}
	}
}
