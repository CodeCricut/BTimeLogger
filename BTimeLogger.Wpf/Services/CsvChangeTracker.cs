using BTimeLogger.Wpf.Controls.Common;
using WpfCore.MessageBus;

namespace BTimeLogger.Csv
{
	// TODO: I feel this might be improperly named as it doesn't keep track if changes have been made to the CSV report, but
	// rather if changes have been made to the data which will eventually be propogated to the report. 
	public interface ICsvChangeTracker
	{
		bool ChangesMade { get; }
		void MakeChange();
		void ClearChanges();
	}

	class CsvChangeTracker : ICsvChangeTracker
	{
		private readonly IEventAggregator _ea;

		public bool ChangesMade { get; private set; }

		public CsvChangeTracker(IEventAggregator ea)
		{
			_ea = ea;
		}

		public void ClearChanges()
		{
			ChangesMade = false;
			_ea.SendMessage(new ChangesMade());
		}

		public void MakeChange()
		{
			ChangesMade = true;
			_ea.SendMessage(new ChangesMade());
		}
	}
}
