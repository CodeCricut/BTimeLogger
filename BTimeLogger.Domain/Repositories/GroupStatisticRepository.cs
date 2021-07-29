using System.Collections.Generic;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Repositories
{
	public interface IGroupStatisticRepository
	{
		Task<GroupStatistic> CreateForGroup(ActivityCode groupType);
		Task<GroupStatistic> CreateForAll();
	}

	class GroupStatisticRepository : IGroupStatisticRepository
	{
		private readonly IStatisticsRepository _statisticsRepository;

		public GroupStatisticRepository(IStatisticsRepository statisticsRepository)
		{
			_statisticsRepository = statisticsRepository;
		}

		public async Task<GroupStatistic> CreateForAll()
		{
			Statistic totalStat = await _statisticsRepository.GetStatsForTotal();

			return await CreateForStat(totalStat);
		}

		public async Task<GroupStatistic> CreateForGroup(ActivityCode groupType)
		{
			if (groupType == null)
				return await CreateForAll();
			Statistic groupStat = await _statisticsRepository.GetStatistic(groupType);
			return await CreateForStat(groupStat);
		}

		private async Task<GroupStatistic> CreateForStat(Statistic statistic)
		{
			Statistic groupParentStat = await _statisticsRepository.GetStatistic(statistic.Activity.Code.ParentCode);

			decimal percentParent = groupParentStat is null ? statistic.Percent : statistic.Percent / groupParentStat.Percent;

			IEnumerable<GroupStatistic> children = await CreateForChildren(statistic.Activity);

			return new GroupStatistic()
			{
				ActivityType = statistic.Activity,
				Duration = statistic.Duration,
				Children = children,
				PercentParent = percentParent,
				PercentTotal = statistic.Percent
			};
		}

		private async Task<IEnumerable<GroupStatistic>> CreateForChildren(Activity activity)
		{
			List<GroupStatistic> childrenGroupStats = new();

			foreach (var childActivity in activity.Children)
			{
				var childGroupStat = await CreateForGroup(childActivity.Code);
				childrenGroupStats.Add(childGroupStat);
			}
			return childrenGroupStats;
		}
	}
}
