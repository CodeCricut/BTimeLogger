using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	class CsvIntervalRepository : IIntervalRepository
	{
		private readonly ICsvReportReader _csvReportReader;

		public CsvIntervalRepository(ICsvReportReader csvReportReader)
		{
			_csvReportReader = csvReportReader;
		}

		public Task<IQueryable<Interval>> GetIntervals()
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<Interval>> GetIntervals(Activity[] activities, DateTime from, DateTime to)
		{
			throw new NotImplementedException();
		}
	}
}
