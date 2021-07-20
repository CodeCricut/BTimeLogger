using BTimeLogger.Csv;
using BTimeLogger.Domain;
using BTimeLogger.Wpf.ViewModels.Messages;
using System;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class CreateReportWindowViewModel : BaseViewModel
	{
		private DateTime _fromDate = DateTime.Today;
		public DateTime FromDate
		{
			get => _fromDate;
			set => Set(ref _fromDate, value);
		}

		private DateTime _toDate = DateTime.Today;
		public DateTime ToDate
		{
			get => _toDate;
			set => Set(ref _toDate, value);
		}

		private string _reportFileLoc;
		private readonly IViewManager _viewManager;
		private readonly IActivityReporter _activityReporter;
		private readonly ICsvPrincipal _csvPrincipal;
		private readonly IEventAggregator _ea;

		public string ReportFileLoc
		{
			get => _reportFileLoc;
			set
			{
				Set(ref _reportFileLoc, value);
				CreateReportCommand.RaiseCanExecuteChanged();
			}
		}

		public DelegateCommand CancelCommand { get; set; }
		public AsyncDelegateCommand CreateReportCommand { get; set; }

		public CreateReportWindowViewModel(IViewManager viewManager,
			IActivityReporter activityReporter,
			ICsvPrincipal csvPrincipal,
			IEventAggregator ea)
		{
			CancelCommand = new DelegateCommand(Cancel);
			CreateReportCommand = new AsyncDelegateCommand(CreateReport, CanCreateReport);
			_viewManager = viewManager;
			_activityReporter = activityReporter;
			_csvPrincipal = csvPrincipal;
			_ea = ea;
		}

		private bool CanCreateReport(object arg)
		{
			return !string.IsNullOrWhiteSpace(_reportFileLoc);
		}

		private async Task CreateReport(object obj)
		{
			try
			{
				// TODO: loading
				_csvPrincipal.CsvFileLocation = ReportFileLoc;
				ActivityReport report = await _activityReporter.Report(FromDate, ToDate);
				_ea.SendMessage(new ActivityReportChanged(report));
			}
			catch (Exception)
			{
				// TODO: error message in creation window
				throw;
			}

		}

		private void Cancel(object obj)
		{
			_viewManager.Close(this);
		}
	}
}
