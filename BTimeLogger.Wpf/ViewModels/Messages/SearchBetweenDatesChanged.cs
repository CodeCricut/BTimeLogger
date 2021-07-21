using System;

namespace BTimeLogger.Wpf.ViewModels.Messages
{
	class SearchBetweenDatesChanged
	{
		public SearchBetweenDatesChanged(DateTime from, DateTime to)
		{
			From = from;
			To = to;
		}

		public DateTime From { get; }
		public DateTime To { get; }
	}
}
