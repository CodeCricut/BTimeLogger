using System;
using System.Threading.Tasks;

namespace BTimeLogger.Domain
{
	public interface IActivityReporter
	{
		Task<ActivityReport> Report(DateTime from, DateTime to);
	}

	public class ActivityReporter : IActivityReporter
	{
		private readonly IStatisticsRepository _statisticsRepository;
		private readonly IIntervalRepository _intervalRepository;

		public ActivityReporter(
			IStatisticsRepository statisticsRepository,
			IIntervalRepository intervalRepository)
		{
			_statisticsRepository = statisticsRepository;
			_intervalRepository = intervalRepository;
		}

		public async Task<ActivityReport> Report(DateTime from, DateTime to)
		{
			//Statistic[] statistics = (await _statisticsRepository.GetStatistics()).ToArray();

			//Interval[] intervals = (await _intervalRepository.GetIntervals())
			//	.BetweenDates(from, to)
			//	.ToArray();

			//return new ActivityReport()
			//{
			//	From = from,
			//	To = to,
			//	Intervals = intervals,
			//	Statistics = statistics
			//};

			return new ActivityReport();
		}
	}
}
