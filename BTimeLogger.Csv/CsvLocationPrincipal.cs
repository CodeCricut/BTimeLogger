namespace BTimeLogger.Csv
{
	public interface ICsvLocationPrincipal
	{
		public bool LocationsAreSelected { get; }
		public string IntervalCsvLocation { get; set; }
		public string StatisticCsvLocation { get; set; }
	}

	class CsvLocationPrincipal : ICsvLocationPrincipal
	{
		public string IntervalCsvLocation { get; set; }
		public string StatisticCsvLocation { get; set; }

		public bool LocationsAreSelected => !(string.IsNullOrWhiteSpace(IntervalCsvLocation) ||
											string.IsNullOrWhiteSpace(StatisticCsvLocation));
	}
}
