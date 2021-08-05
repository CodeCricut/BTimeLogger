using BTimeLogger.Csv;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Controls
{
	public interface ICurrentReportBannerViewModelFactory
	{
		CurrentReportBannerViewModel Create();
	}

	class CurrentReportBannerViewModelFactory : ICurrentReportBannerViewModelFactory
	{
		private readonly ICsvLocationPrincipal _csvLocationPrincipal;
		private readonly ICsvChangeTracker _csvChangeTracker;
		private readonly IEventAggregator _ea;

		public CurrentReportBannerViewModelFactory(
			ICsvLocationPrincipal csvLocationPrincipal,
			ICsvChangeTracker csvChangeTracker,
			IEventAggregator ea)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_csvChangeTracker = csvChangeTracker;
			_ea = ea;
		}

		public CurrentReportBannerViewModel Create()
		{
			return new CurrentReportBannerViewModel(_csvLocationPrincipal, _csvChangeTracker, _ea);
		}
	}
}
