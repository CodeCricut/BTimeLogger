using BTimeLogger.Wpf.Controls.Common;
using WpfCore.MessageBus;

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
