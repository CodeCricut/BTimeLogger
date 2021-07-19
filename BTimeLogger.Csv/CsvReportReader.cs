namespace BTimeLogger.Csv
{
	// TODO: csv should only be read once, perhaps with the option to refresh results
	interface ICsvReportReader
	{
		Interval[] ReadIntervals();
		Activity[] ReadActivities();
		Statistic[] ReadStatistics();
	}

	class CsvReportReader : ICsvReportReader
	{
		private readonly ICsvPrincipal _csvPrincipal;

		public CsvReportReader(ICsvPrincipal csvPrincipal)
		{
			_csvPrincipal = csvPrincipal;
		}

		public Activity[] ReadActivities()
		{
			throw new System.NotImplementedException();
		}

		public Interval[] ReadIntervals()
		{
			throw new System.NotImplementedException();
		}

		public Statistic[] ReadStatistics()
		{
			throw new System.NotImplementedException();
		}
	}
}
