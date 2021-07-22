using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IIntervalsViewModelFactory
	{
		IntervalsViewModel Create(IntervalListViewModel intervalListViewModel, GroupedActivityFilterViewModel groupedActivityFilterViewModel);
	}

	class IntervalsViewModelFactory : IIntervalsViewModelFactory
	{
		public IntervalsViewModel Create(IntervalListViewModel intervalListViewModel, GroupedActivityFilterViewModel groupedActivityFilterViewModel)
		{
			return new IntervalsViewModel(intervalListViewModel, groupedActivityFilterViewModel);
		}
	}
}
