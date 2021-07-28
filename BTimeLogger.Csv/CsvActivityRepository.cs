//using BTimeLogger.Domain;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace BTimeLogger.Csv
//{
//	class CsvActivityRepository : IActivityRepository
//	{
//		private readonly IIntervalsCsvReader _reportReader;

//		public CsvActivityRepository(IIntervalsCsvReader reportReader)
//		{
//			_reportReader = reportReader;
//		}

//		public Task<bool> ActivityExists(string name)
//		{
//			throw new NotImplementedException();
//		}

//		public async Task<IEnumerable<Activity>> GetActivities()
//		{
//			IEnumerable<Activity> activities = await _reportReader.ReadActivites();
//			return activities;
//		}

//		public Task<Activity> GetActivity(string name)
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
