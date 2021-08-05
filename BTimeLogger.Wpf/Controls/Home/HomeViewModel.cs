using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel(OpenRecentReportListViewModel openRecentReportListViewModel)
		{
			OpenRecentReportListViewModel = openRecentReportListViewModel;
		}

		public OpenRecentReportListViewModel OpenRecentReportListViewModel { get; }
	}
}
