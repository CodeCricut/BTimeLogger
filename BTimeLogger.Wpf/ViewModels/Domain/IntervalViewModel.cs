using BTimeLogger.Wpf.Util;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.Domain
{
	public class IntervalViewModel : BaseViewModel
	{
		private readonly Interval _interval;

		public string Duration { get => _interval.Duration.DurationString(); }

		public string From { get => _interval.From.DateTimeString(); }
		public string To { get => _interval.To.DateTimeString(); }

		public string Comment { get => _interval.Comment; }
		public bool HasComment { get => !string.IsNullOrWhiteSpace(_interval.Comment); }

		public string FromDate { get => _interval.From.ToShortDateString(); }

		public ActivityViewModel Activity { get; private set; }

		public IntervalViewModel(Interval interval, ActivityViewModel intervalActivityVM)
		{
			_interval = interval;
			Activity = intervalActivityVM;
		}
	}
}
