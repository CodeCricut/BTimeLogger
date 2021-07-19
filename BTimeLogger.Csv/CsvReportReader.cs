namespace BTimeLogger.Csv
{
	// TODO: csv should only be read once, perhaps with the option to refresh results
	interface ICsvReportReader
	{
		Interval[] ReadIntervals(string csvReportFileLoc);
		Activity[] ReadActivities(string csvReportFileLoc);
		Statistic[] ReadStatistics(string csvReportFileLoc);
	}

	class CsvReportReader : ICsvReportReader
	{
		public Activity[] ReadActivities(string csvReportFileLoc)
		{
			throw new System.NotImplementedException();
		}

		public Interval[] ReadIntervals(string csvReportFileLoc)
		{
			throw new System.NotImplementedException();
		}

		public Statistic[] ReadStatistics(string csvReportFileLoc)
		{
			throw new System.NotImplementedException();
		}
	}
}
