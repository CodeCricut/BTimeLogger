namespace BTimeLogger.Wpf.Controls
{
	public interface IHomeViewModelFactory
	{
		HomeViewModel Create();
	}

	class HomeViewModelFactory : IHomeViewModelFactory
	{
		private readonly IOpenRecentReportListViewModelFactory _openRecentReportListViewModelFactory;

		public HomeViewModelFactory(IOpenRecentReportListViewModelFactory openRecentReportListViewModelFactory)
		{
			_openRecentReportListViewModelFactory = openRecentReportListViewModelFactory;
		}
		public HomeViewModel Create()
		{
			OpenRecentReportListViewModel openRecentVM = _openRecentReportListViewModelFactory.Create();
			return new HomeViewModel(openRecentVM);
		}
	}
}
