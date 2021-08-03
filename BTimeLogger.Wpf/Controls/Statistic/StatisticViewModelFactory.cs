namespace BTimeLogger.Wpf.Controls
{
	public interface IStatisticViewModelFactory
	{
		StatisticViewModel Create(Statistic statistic);
	}

	class StatisticViewModelFactory : IStatisticViewModelFactory
	{
		public StatisticViewModel Create(Statistic statistic)
		{
			return new(statistic);
		}
	}
}
