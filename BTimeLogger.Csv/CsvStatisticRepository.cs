using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	class CsvStatisticRepository : IStatisticsRepository
	{
		private readonly IIntervalsCsvReader _csvReportReader;

		public CsvStatisticRepository(IIntervalsCsvReader csvReportReader)
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
