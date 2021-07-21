namespace BTimeLogger.Wpf.ViewModels.Messages
{
	class CsvLocationChanged
	{
		public CsvLocationChanged(string newLocation)
		{
			NewLocation = newLocation;
		}

		public string NewLocation { get; }
	}
}
