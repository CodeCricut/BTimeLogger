using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger
{
	public interface IStatisticsRepository
	{
		Task<IQueryable<Statistic>> GetStatistics();
		Task<Statistic> GetStatistic(string activityName);
		Task<bool> StatisticExists(string activityName);
	}
}
