using BTimeLogger.Csv;
using BTimeLogger.Csv.Services;
using BTimeLogger.Wpf.Controls.Common;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class CurrentReportBannerViewModel : BaseViewModel
	{
		private const string UnsavedReportLocation = "Unsaved";

		private readonly ICsvLocationPrincipal _csvLocationPrincipal;
		private readonly ICsvChangeTracker _csvChangeTracker;

		private string _currentReportLocation = UnsavedReportLocation;
		public string CurrentReportLocation
		{
			get => _currentReportLocation;
			set { Set(ref _currentReportLocation, value); }
		}

		private bool _hasUnsavedChanges;
		public bool HasUnsavedChanges
		{
			get => _hasUnsavedChanges;
			set { Set(ref _hasUnsavedChanges, value); }
		}

		public CurrentReportBannerViewModel(ICsvLocationPrincipal csvLocationPrincipal,
			ICsvChangeTracker csvChangeTracker,
			IEventAggregator ea)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_csvChangeTracker = csvChangeTracker;

			ea.RegisterHandler<ReportSourceChanged>(msg => UpdateCurrentReportLocation());
			ea.RegisterHandler<ChangesMade>(msg => UpdateHasUnsavedChanges());
		}

		private void UpdateCurrentReportLocation()
		{
			if (_csvLocationPrincipal.LocationsAreSelected)
				CurrentReportLocation = _csvLocationPrincipal.CsvLocation;
			else CurrentReportLocation = UnsavedReportLocation;
		}

		private void UpdateHasUnsavedChanges()
		{
			HasUnsavedChanges = _csvChangeTracker.ChangesMade;
		}
	}
}
