using System;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class CreateReportWindowViewModel : BaseViewModel
	{
		private DateTime _fromDate;
		public DateTime FromDate
		{
			get => _fromDate;
			set => Set(ref _fromDate, value);
		}

		private DateTime _toDate;
		public DateTime ToDate
		{
			get => _toDate;
			set => Set(ref _toDate, value);
		}

		private string _reportFileLoc;
		private readonly IViewManager _viewManager;

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
		public DelegateCommand CreateReportCommand { get; set; }

		public CreateReportWindowViewModel(IViewManager viewManager)
		{
			CancelCommand = new DelegateCommand(Cancel);
			CreateReportCommand = new DelegateCommand(CreateReport, CanCreateReport);
			_viewManager = viewManager;
		}

		private bool CanCreateReport(object arg)
		{
			return !string.IsNullOrWhiteSpace(_reportFileLoc);
		}

		private void CreateReport(object obj)
		{
			throw new NotImplementedException();
		}

		private void Cancel(object obj)
		{
			_viewManager.Close(this);
		}
	}
}
