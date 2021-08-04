namespace BTimeLogger.Csv
{
	public interface ICsvChangeTracker
	{
		bool ChangesMade { get; }
		void MakeChange();
		void ClearChanges();
	}

	class CsvChangeTracker : ICsvChangeTracker
	{
		public bool ChangesMade { get; private set; }

		public void ClearChanges()
		{
			ChangesMade = false;
		}

		public void MakeChange()
		{
			ChangesMade = true;
		}
	}
}
