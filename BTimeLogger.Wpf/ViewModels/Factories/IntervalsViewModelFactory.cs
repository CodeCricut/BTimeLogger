using BTimeLogger.Wpf.ViewModels.MainWindow;
using WpfCore.MessageBus;

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
		private readonly IEventAggregator _ea;

		public IntervalsViewModelFactory(IEventAggregator ea)
		{
			_ea = ea;
		}

		public IntervalsViewModel Create(
			PartialIntervalListViewModel partialIntervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			return new IntervalsViewModel(partialIntervalListViewModel, groupedActivityFilterViewModel, timeSpanPanelViewModel, _ea);
		}
	}
}
