using BTimeLogger.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	class CsvActivityRepository : IActivityRepository
	{
		private readonly string _csvReportPath;
		private readonly ICsvReportReader _reportReader;

		public CsvActivityRepository(string csvReportPath, ICsvReportReader reportReader)
		{
			_csvReportPath = csvReportPath;
			_reportReader = reportReader;
		}

		public Task<bool> ActivityExists(string name)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<Activity>> GetActivities()
		{
			throw new NotImplementedException();
			return Task.FromResult(_reportReader.ReadActivities(_csvReportPath).AsQueryable());
		}

		public Task<Activity> GetActivity(string name)
		{
			throw new NotImplementedException();
		}
	}
}
