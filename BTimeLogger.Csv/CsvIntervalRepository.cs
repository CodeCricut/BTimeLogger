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

		public async Task<IQueryable<Interval>> GetIntervals(Activity[] activities, DateTime from, DateTime to)
		{
			await Task.Delay(10000);
			// TODO
			Interval interval = new Interval()
			{
				From = DateTime.Now,
				To = DateTime.Now,
				Activity = new Activity()
				{
					Children = Array.Empty<Activity>(),
					IsGroup = false,
					Name = "BOOB",
					Parent = null
				},
				Comment = "this is a comment :)",
				Duration = new TimeSpan(1, 2, 3)
			};
			return new Interval[] { interval }.AsQueryable();
		}
	}
}
