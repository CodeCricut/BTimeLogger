using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IIntervalsViewModelFactory
	{
		IntervalsViewModel Create(IntervalListViewModel intervalListViewModel, GroupedActivityFilterViewModel groupedActivityFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel);
	}

	class IntervalsViewModelFactory : IIntervalsViewModelFactory
	{
		public IntervalsViewModel Create(IntervalListViewModel intervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			return new IntervalsViewModel(intervalListViewModel, groupedActivityFilterViewModel, timeSpanPanelViewModel);
		}
	}
}
