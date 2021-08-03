using BTimeLogger.Wpf.ViewModels.Domain;

namespace BTimeLogger.Wpf.ViewModels.Factories
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
