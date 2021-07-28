using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	public interface IStatisticsCsvReader
	{
		Task ReadStatisticsCsv(string statisticsCsvLocation);
	}

	class StatisticsCsvReader : IStatisticsCsvReader
	{
		private readonly IStatisticsRepository _statisticsRepository;

		public StatisticsCsvReader(IStatisticsRepository statisticsRepository)
		{
			_statisticsRepository = statisticsRepository;
		}

		public Task ReadStatisticsCsv(string statisticsCsvLocation)
		{
			// TODO;
			return Task.CompletedTask;
		}
	}
}
