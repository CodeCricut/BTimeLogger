using BTimeLogger.Wpf.Services.AppData;

namespace BTimeLogger.Wpf.Controls
{
	public interface IHomeViewModelFactory
	{
		HomeViewModel Create();
	}

	class HomeViewModelFactory : IHomeViewModelFactory
	{
		private readonly IReportLocationsPrincipal _reportLocationsPrincipal;

		public HomeViewModelFactory(IReportLocationsPrincipal reportLocationsPrincipal)
		{
			_reportLocationsPrincipal = reportLocationsPrincipal;
		}
		public HomeViewModel Create()
		{
			return new HomeViewModel(_reportLocationsPrincipal);
		}
	}
}
