namespace BTimeLogger.Csv
{
	public interface ICsvPrincipal
	{
		string CsvFileLocation { get; set; }
	}

	class CsvPrincipal : ICsvPrincipal
	{
		public string CsvFileLocation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
	}
}
