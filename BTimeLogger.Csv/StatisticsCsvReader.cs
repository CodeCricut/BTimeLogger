using System;

namespace BTimeLogger.Csv
{
	public interface IStatisticsCsvReader
	{
		Statistic[] ReadStatistics();
	}

	class StatisticsCsvReader : IStatisticsCsvReader
	{
		private readonly ICsvPrincipal _csvPrincipal;

		public StatisticsCsvReader(ICsvPrincipal csvPrincipal)
		{
			_csvPrincipal = csvPrincipal;
		}

		public Statistic[] ReadStatistics()
		{
			throw new NotImplementedException();
		}
	}
}
