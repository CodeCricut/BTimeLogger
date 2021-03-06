using BTimeLogger.Csv.Helpers;

namespace BTimeLogger.Wpf.Controls;

public class StatisticViewModel : BaseViewModel
{
	private readonly Statistic _statistic;

	public ActivityViewModel Activity { get; set; }
	public string Duration { get => _statistic.Duration.ToCsvFormat(); }
	public decimal Percent { get => _statistic.PercentOfTrackedTimeInTimespan; }

	public StatisticViewModel(Statistic statistic)
	{
		_statistic = statistic;
	}
}
