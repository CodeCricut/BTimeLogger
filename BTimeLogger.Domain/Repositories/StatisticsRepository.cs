using BTimeLogger.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger
{
	public interface IStatisticsRepository
	{
		Task<IEnumerable<Statistic>> GetStatistics();
		Task<Statistic> GetStatistic(ActivityCode activityCode);
		Task<IEnumerable<Statistic>> GetStatistics(ActivityCode[] activityCodes);
		Task<bool> StatisticExists(ActivityCode activityCode);
		Task AddStatistic(Statistic statistic);
		Task<Statistic> GetStatsForTotal(string totalActivityName = "Total");
	}

	class StatisticsRepository : IStatisticsRepository
	{
		private const int TOTAL_PERCENT = 1;
		private readonly Dictionary<ActivityCode, Statistic> _statistics = new();

		public Task AddStatistic(Statistic statistic)
		{
			return Task.Factory.StartNew(() =>
				_statistics.Add(statistic.Activity.Code, statistic)
			);
		}


		public Task<Statistic> GetStatistic(ActivityCode activityCode)
		{
			return Task.FromResult(_statistics.GetValueOrDefault(activityCode));
		}

		public Task<IEnumerable<Statistic>> GetStatistics()
		{
			return Task.FromResult(_statistics.Select(kvp => kvp.Value));
		}

		public async Task<IEnumerable<Statistic>> GetStatistics(ActivityCode[] activityCodes)
		{
			IEnumerable<Statistic> allStats = await GetStatistics();
			return allStats.Where(stat => activityCodes.Contains(stat.Activity.Code));
		}

		public async Task<Statistic> GetStatsForTotal(string totalActivityName = "Total")
		{
			IEnumerable<Statistic> allStats = await GetStatistics();
			IEnumerable<Statistic> parentlessStats = allStats.Where(stat => !stat.Activity.HasParent);

			TimeSpan totalDuration = parentlessStats.TotalDuration();

			Activity totalActivity = new Activity()
			{
				Children = parentlessStats.Select(stat => stat.Activity),
				IsGroup = true,
				Name = totalActivityName,
				Parent = null
			};

			return new Statistic()
			{
				Activity = totalActivity,
				Duration = totalDuration,
				Percent = TOTAL_PERCENT
			};
		}

		public Task<bool> StatisticExists(ActivityCode activityCode)
		{
			return Task.FromResult(_statistics.ContainsKey(activityCode));
		}
	}
}
