namespace BTimeLogger.Csv
{
	public interface ICsvPrincipal
	{
		string IntervalsCsvLocation { get; set; }
		string StatisticsCsvLocation { get; set; }

	}

	class CsvPrincipal : ICsvPrincipal
	{
		public string IntervalsCsvLocation { get; set; }
		public string StatisticsCsvLocation { get; set; }
	}
}
