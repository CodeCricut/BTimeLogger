using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	class CsvStatisticRepository : IStatisticsRepository
	{
		private readonly ICsvReportReader _csvReportReader;

		public CsvStatisticRepository(ICsvReportReader csvReportReader)
		{
			_csvReportReader = csvReportReader;
		}

		public Task<Statistic> GetStatistic(string activityName)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<Statistic>> GetStatistics()
		{
			throw new NotImplementedException();
		}

		public Task<bool> StatisticExists(string activityName)
		{
			throw new NotImplementedException();
		}
	}
}
