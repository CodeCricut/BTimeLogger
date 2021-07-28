using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger
{
	public interface IStatisticsRepository
	{
		Task<IEnumerable<Statistic>> GetStatistics();
		Task<Statistic> GetStatistic(string activityName);
		Task<bool> StatisticExists(string activityName);
		Task AddStatistic(Statistic statistic);
	}

	class StatisticsRepository : IStatisticsRepository
	{
		private readonly Dictionary<string, Statistic> _statistics = new();

		public Task AddStatistic(Statistic statistic)
		{
			return Task.Factory.StartNew(() =>
				_statistics.Add(statistic.Activity.Name, statistic)
			);
		}

		public Task<Statistic> GetStatistic(string activityName)
		{
			return Task.FromResult(_statistics.GetValueOrDefault(activityName));
		}

		public Task<IEnumerable<Statistic>> GetStatistics()
		{
			return Task.FromResult(_statistics.Select(kvp => kvp.Value));
		}

		public Task<bool> StatisticExists(string activityName)
		{
			return Task.FromResult(_statistics.ContainsKey(activityName));
		}
	}
}
