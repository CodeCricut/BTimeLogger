//using BTimeLogger.Domain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BTimeLogger.Csv
//{
//	class CsvIntervalRepository : IIntervalRepository
//	{
//		private readonly IIntervalsCsvReader _csvReportReader;

//		private readonly TimedRequeryBehavior _requeryBehavior = new(TimeSpan.FromSeconds(10));
//		public CsvIntervalRepository(IIntervalsCsvReader csvReportReader)
//		{
//			_csvReportReader = csvReportReader;
//		}

//		private IEnumerable<Interval> _intervals = Array.Empty<Interval>();


//		public async Task<IEnumerable<Interval>> GetIntervals()
//		{
//			if (_requeryBehavior.ShouldRequery)
//			{
//				_requeryBehavior.Requery();
//				_intervals = await _csvReportReader.ReadIntervals();
//			}
//			return _intervals;
//		}

//		public async Task<IEnumerable<Interval>> GetIntervals(Activity[] activities, DateTime from, DateTime to)
//		{

//			var allIntervals = await GetIntervals();

//			var intervalsInTimeRange = allIntervals.Where(interval => interval.To > from || interval.From < to);

//			if (activities.Length == 0) return intervalsInTimeRange;

//			var intervalsWithActivities = intervalsInTimeRange.Where(interval => activities.Contains(interval.Activity, new ActivityNameEqualityOperator()));
//			return intervalsWithActivities;
//		}
//	}
//}
