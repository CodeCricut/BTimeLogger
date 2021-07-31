using BTimeLogger.Wpf.Util;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.Domain
{
	public class StatisticViewModel : BaseViewModel
	{
		private readonly Statistic _statistic;

		public ActivityViewModel Activity { get; set; }
		public string Duration { get => _statistic.Duration.DurationString(); }
		public decimal Percent { get => _statistic.PercentOfTrackedTimeInTimespan; }

		public StatisticViewModel(Statistic statistic)
		{
			_statistic = statistic;
		}
	}
}
