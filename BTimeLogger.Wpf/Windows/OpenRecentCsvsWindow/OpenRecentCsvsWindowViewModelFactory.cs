using BTimeLogger.Wpf.Controls;
using WpfCore.MessageBus;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Windows
{
	public interface IOpenRecentCsvsWindowViewModelFactory
	{
		OpenRecentCsvsWindowViewModel Create();
	}

	class OpenRecentCsvsWindowViewModelFactory : IOpenRecentCsvsWindowViewModelFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IEventAggregator _ea;
		private readonly IWindowButtonsViewModelFactory _windowButtonsViewModelFactory;
		private readonly IOpenRecentReportListViewModelFactory _openRecentReportListViewModelFactory;

		public OpenRecentCsvsWindowViewModelFactory(IViewManager viewManager,
											  IEventAggregator ea,
											  IWindowButtonsViewModelFactory windowButtonsViewModelFactory,
											  IOpenRecentReportListViewModelFactory openRecentReportListViewModelFactory)
		{
			_viewManager = viewManager;
			_ea = ea;
			_windowButtonsViewModelFactory = windowButtonsViewModelFactory;
			_openRecentReportListViewModelFactory = openRecentReportListViewModelFactory;
		}

		public OpenRecentCsvsWindowViewModel Create()
		{
			WindowButtonsViewModel windowButtonsViewModel = _windowButtonsViewModelFactory.Create();
			OpenRecentReportListViewModel openRecentVM = _openRecentReportListViewModelFactory.Create();

			return new OpenRecentCsvsWindowViewModel(_viewManager, _ea, windowButtonsViewModel, openRecentVM);
		}
	}
}
