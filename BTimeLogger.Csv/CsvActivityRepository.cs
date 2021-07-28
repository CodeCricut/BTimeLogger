using BTimeLogger.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	class CsvActivityRepository : IActivityRepository
	{
		private readonly IIntervalsCsvReader _reportReader;

		public CsvActivityRepository(IIntervalsCsvReader reportReader)
		{
			_reportReader = reportReader;
		}

		public Task<bool> ActivityExists(string name)
		{
			throw new NotImplementedException();
		}

		public async Task<IQueryable<Activity>> GetActivities()
		{
			// TODO:
			await _reportReader.ReadDataAsync();
			return _reportReader.Activities.AsQueryable();
		}

		public Task<Activity> GetActivity(string name)
		{
			throw new NotImplementedException();
		}
	}
}
