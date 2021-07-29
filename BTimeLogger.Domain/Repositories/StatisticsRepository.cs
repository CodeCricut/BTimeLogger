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
		Task<bool> StatisticExists(ActivityCode activityCode);
		Task AddStatistic(Statistic statistic);
	}

	class StatisticsRepository : IStatisticsRepository
	{
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

		public Task<bool> StatisticExists(ActivityCode activityCode)
		{
			return Task.FromResult(_statistics.ContainsKey(activityCode));
		}
	}
}
