namespace BTimeLogger.Csv
{
	public interface ICsvPrincipal
	{
		string CsvFileLocation { get; set; }
	}

	class CsvPrincipal : ICsvPrincipal
	{
		// TODO: file location verification
		public string CsvFileLocation { get; set; }
	}
}
