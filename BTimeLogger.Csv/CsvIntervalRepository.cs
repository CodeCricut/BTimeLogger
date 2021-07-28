using BTimeLogger.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	class CsvIntervalRepository : IIntervalRepository
	{
		private readonly IIntervalsCsvReader _csvReportReader;

		public CsvIntervalRepository(IIntervalsCsvReader csvReportReader)
		{
			_csvReportReader = csvReportReader;
		}

		public async Task<IQueryable<Interval>> GetIntervals()
		{
			await _csvReportReader.ReadDataAsync();
			return _csvReportReader.Intervals.AsQueryable();
		}

		public async Task<IQueryable<Interval>> GetIntervals(Activity[] activities, DateTime from, DateTime to)
		{
			var allIntervals = await GetIntervals();

			if (activities.Length == 0) return allIntervals;
			var matchingActivities = allIntervals.Where(interval => activities.Contains(interval.Activity, new ActivityNameEqualityOperator()))
				.Where(interval => interval.To > from || interval.From < to);
			return matchingActivities;
		}
	}
}
