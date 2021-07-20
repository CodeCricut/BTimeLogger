using BTimeLogger.Wpf.Util;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.Domain
{
	public class IntervalViewModel : BaseViewModel
	{
		private readonly Interval _interval;

		public string Duration { get => _interval.Duration.DurationString(); }
		public string From { get => _interval.From.DateString(); }
		public string To { get => _interval.To.DateString(); }
		public string Comment { get => _interval.Comment; }
		public bool HasComment { get => !string.IsNullOrWhiteSpace(_interval.Comment); }
		public IntervalViewModel(Interval interval)
		{
			_interval = interval;
		}
	}
}
