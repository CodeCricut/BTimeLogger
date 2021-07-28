using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IIntervalsViewModelFactory
	{
		IntervalsViewModel Create(
			PartialIntervalListViewModel partialIntervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel);
	}

	class IntervalsViewModelFactory : IIntervalsViewModelFactory
	{
		public IntervalsViewModel Create(
			PartialIntervalListViewModel partialIntervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			return new IntervalsViewModel(partialIntervalListViewModel, groupedActivityFilterViewModel, timeSpanPanelViewModel);
		}
	}
}
