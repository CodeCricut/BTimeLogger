using BTimeLogger.Csv.Helpers;
using BTimeLogger.Wpf.Util;

namespace BTimeLogger.Wpf.Controls;

public class IntervalViewModel : BaseViewModel
{
	public Interval Interval { get; }

	public string Duration { get => Interval.Duration.ToCsvFormat(); }

	public string From { get => Interval.From.DateTimeString(); }
	public string To { get => Interval.To.DateTimeString(); }

	public string Comment { get => Interval.Comment; }
	public bool HasComment { get => !string.IsNullOrWhiteSpace(Interval.Comment); }

	public string FromDate { get => Interval.From.ToShortDateString(); }

	public ActivityViewModel Activity { get; private set; }

	public IntervalViewModel(Interval interval, ActivityViewModel intervalActivityVM)
	{
		Interval = interval;
		Activity = intervalActivityVM;
	}
}
