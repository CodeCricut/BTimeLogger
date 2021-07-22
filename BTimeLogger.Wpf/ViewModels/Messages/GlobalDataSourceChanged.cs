namespace BTimeLogger.Wpf.ViewModels.Messages
{
	class GlobalDataSourceChanged
	{
		public GlobalDataSourceChanged(string newLocation)
		{
			NewLocation = newLocation;
		}

		public string NewLocation { get; }
	}
}
