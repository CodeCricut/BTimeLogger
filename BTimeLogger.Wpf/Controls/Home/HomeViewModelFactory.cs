using BTimeLogger.Wpf.Windows;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Controls
{
	public interface IHomeViewModelFactory
	{
		HomeViewModel Create();
	}

	class HomeViewModelFactory : IHomeViewModelFactory
	{
		private readonly IOpenRecentReportListViewModelFactory _openRecentReportListViewModelFactory;
		private readonly ICreateNewProjectWindowViewModelFactory _createNewProjectWindowViewModelFactory;
		private readonly IViewManager _viewManager;

		public HomeViewModelFactory(IOpenRecentReportListViewModelFactory openRecentReportListViewModelFactory,
			ICreateNewProjectWindowViewModelFactory createNewProjectWindowViewModelFactory,
			IViewManager viewManager)
		{
			_openRecentReportListViewModelFactory = openRecentReportListViewModelFactory;
			_createNewProjectWindowViewModelFactory = createNewProjectWindowViewModelFactory;
			_viewManager = viewManager;
		}
		public HomeViewModel Create()
		{
			OpenRecentReportListViewModel openRecentVM = _openRecentReportListViewModelFactory.Create();
			return new HomeViewModel(openRecentVM, _createNewProjectWindowViewModelFactory, _viewManager);
		}
	}
}
