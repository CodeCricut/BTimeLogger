﻿using BTimeLogger.Csv;
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
		private readonly IViewManager _viewManager;
		private readonly IIntervalsCsvReader _intervalsCsvReader;
		private readonly IStatisticsCsvReader _statisticsCsvReader;
		private readonly IEventAggregator _ea;

		private DateTime _fromDate = DateTime.Today;
		public DateTime FromDate { get => _fromDate; set => Set(ref _fromDate, value); }

		private DateTime _toDate = DateTime.Today;
		public DateTime ToDate { get => _toDate; set => Set(ref _toDate, value); }


		private string _intervalsCsvLocation;
		public string IntervalsCsvLocation
		{
			get => _intervalsCsvLocation;
			set { Set(ref _intervalsCsvLocation, value); CreateReportCommand.RaiseCanExecuteChanged(); }
		}

		private string _statisticsCsvLocation;
		public string StatisticsCsvLocation
		{
			get => _statisticsCsvLocation;
			set { Set(ref _statisticsCsvLocation, value); CreateReportCommand.RaiseCanExecuteChanged(); }
		}

		private bool _loading;
		public bool Loading { get => _loading; set { Set(ref _loading, value); RaisePropertyChanged(nameof(InvalidReportInfo)); } }

		private bool _invalidReportInfo;
		public bool InvalidReportInfo { get => _invalidReportInfo && !Loading; set { Set(ref _invalidReportInfo, value); } }


		public DelegateCommand CancelCommand { get; set; }
		public AsyncDelegateCommand CreateReportCommand { get; set; }

		public CreateReportWindowViewModel(IViewManager viewManager,
			IIntervalsCsvReader intervalsCsvReader,
			IStatisticsCsvReader statisticsCsvReader,
			IEventAggregator ea)
		{
			CancelCommand = new DelegateCommand(Cancel);
			CreateReportCommand = new AsyncDelegateCommand(CreateReport, CanCreateReport);
			_viewManager = viewManager;
			_intervalsCsvReader = intervalsCsvReader;
			_statisticsCsvReader = statisticsCsvReader;
			_ea = ea;
		}

		private bool CanCreateReport(object arg)
		{
			return !(string.IsNullOrWhiteSpace(StatisticsCsvLocation) || string.IsNullOrWhiteSpace(IntervalsCsvLocation));
		}

		private async Task CreateReport(object obj)
		{
			try
			{
				Loading = true;
				InvalidReportInfo = false;

				await _intervalsCsvReader.ReadIntervalCsv(IntervalsCsvLocation);
				await _statisticsCsvReader.ReadStatisticsCsv(StatisticsCsvLocation);

				_ea.SendMessage(new ReportSourceChanged());

				Loading = false;
				CloseDialog();
			}
			catch (Exception e)
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
