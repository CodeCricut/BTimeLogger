using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IIntervalsViewModelFactory
	{
		IntervalsViewModel Create(IntervalListViewModel intervalListViewModel);
	}

	class IntervalsViewModelFactory : IIntervalsViewModelFactory
	{
		public IntervalsViewModel Create(IntervalListViewModel intervalListViewModel)
		{
			return new IntervalsViewModel(intervalListViewModel);
		}
	}
}
