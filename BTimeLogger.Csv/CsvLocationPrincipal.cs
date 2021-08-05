using System.IO;

namespace BTimeLogger.Csv
{
	public interface ICsvLocationPrincipal
	{
		public bool LocationsAreSelected { get; }
		public string CsvLocation { get; set; }
	}

	class CsvLocationPrincipal : ICsvLocationPrincipal
	{
		private string _csvLocation;
		public string CsvLocation
		{
			get => _csvLocation;
			set
			{
				if (!File.Exists(value)) throw new FileNotFoundException("The CSV file location does not exist.", nameof(value));
				_csvLocation = value;
			}
		}

		public bool LocationsAreSelected => !string.IsNullOrWhiteSpace(CsvLocation);
	}
}
