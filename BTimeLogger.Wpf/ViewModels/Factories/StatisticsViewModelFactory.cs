using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IStatisticsViewModelFactory
	{
		StatisticsViewModel Create();
	}

	class StatisticsViewModelFactory : IStatisticsViewModelFactory
	{
		public StatisticsViewModel Create()
		{
			return new();
		}
	}
}
