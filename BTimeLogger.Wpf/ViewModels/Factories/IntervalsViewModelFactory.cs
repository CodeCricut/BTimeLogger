using BTimeLogger.Wpf.ViewModels.MainWindow;
using WpfCore.MessageBus;
using WpfCore.Services;

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
		private readonly ICreateNewIntervalWindowViewModelFactory _createNewIntervalWindowViewModelFactory;
		private readonly IViewManager _viewManager;

		public IntervalsViewModelFactory(IEventAggregator ea,
			ICreateNewIntervalWindowViewModelFactory createNewIntervalWindowViewModelFactory,
			IViewManager viewManager)
		{
			_ea = ea;
			_createNewIntervalWindowViewModelFactory = createNewIntervalWindowViewModelFactory;
			_viewManager = viewManager;
		}

		public IntervalsViewModel Create(
			PartialIntervalListViewModel partialIntervalListViewModel,
			GroupedActivityFilterViewModel groupedActivityFilterViewModel,
			TimeSpanPanelViewModel timeSpanPanelViewModel)
		{
			return new IntervalsViewModel(partialIntervalListViewModel, groupedActivityFilterViewModel, timeSpanPanelViewModel,
				_createNewIntervalWindowViewModelFactory, _viewManager, _ea);
		}
	}
}
