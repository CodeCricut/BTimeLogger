using BTimeLogger.Csv;
using BTimeLogger.Wpf.ViewModels.Messages;
using System;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class CreateReportWindowViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;
		private readonly ICsvPrincipal _csvPrincipal;
		private readonly IEventAggregator _ea;

		private DateTime _fromDate = DateTime.Today;
		public DateTime FromDate { get => _fromDate; set => Set(ref _fromDate, value); }

		private DateTime _toDate = DateTime.Today;
		public DateTime ToDate { get => _toDate; set => Set(ref _toDate, value); }

		private string _reportFileLoc;
		public string ReportFileLoc { get => _reportFileLoc; set { Set(ref _reportFileLoc, value); CreateReportCommand.RaiseCanExecuteChanged(); } }

		private bool _loading;
		public bool Loading { get => _loading; set { Set(ref _loading, value); RaisePropertyChanged(nameof(InvalidReportInfo)); } }

		private bool _invalidReportInfo;
		public bool InvalidReportInfo { get => _invalidReportInfo && !Loading; set { Set(ref _invalidReportInfo, value); } }


		public DelegateCommand CancelCommand { get; set; }
		public DelegateCommand CreateReportCommand { get; set; }

		public CreateReportWindowViewModel(IViewManager viewManager,
			ICsvPrincipal csvPrincipal,
			IEventAggregator ea)
		{
			CancelCommand = new DelegateCommand(Cancel);
			CreateReportCommand = new DelegateCommand(CreateReport, CanCreateReport);
			_viewManager = viewManager;
			_csvPrincipal = csvPrincipal;
			_ea = ea;
		}

		private bool CanCreateReport(object arg)
		{
			return !string.IsNullOrWhiteSpace(_reportFileLoc);
		}

		private void CreateReport(object obj)
		{
			try
			{
				Loading = true;
				InvalidReportInfo = false;

				_csvPrincipal.CsvFileLocation = ReportFileLoc;

				_ea.SendMessage(new GlobalDataSourceChanged(ReportFileLoc));

				Loading = false;
				CloseDialog();
			}
			catch (Exception)
			{
				Loading = false;
				InvalidReportInfo = true;
			}

		}

		private void Cancel(object obj)
		{
			CloseDialog();
		}

		private void CloseDialog()
		{
			_viewManager.Close(this);
		}
	}
}
