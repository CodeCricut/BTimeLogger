using BTimeLogger.Wpf.Mediator;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Windows;

public class OpenCsvsWindowViewModel : BaseViewModel
{
	private readonly IViewManager _viewManager;
	private readonly IMediator _mediator;
	private string _intervalsCsvLocation;
	public string IntervalsCsvLocation
	{
		get => _intervalsCsvLocation;
		set { Set(ref _intervalsCsvLocation, value); CreateReportCommand.RaiseCanExecuteChanged(); }
	}

	private bool _loading;
	public bool Loading
	{
		get => _loading; set
		{
			Set(ref _loading, value);
			RaisePropertyChanged(nameof(InvalidReportInfo));
			RaisePropertyChanged(nameof(NotLoading));
		}
	}

	public bool NotLoading { get => !Loading; }

	private bool _invalidReportInfo;
	public bool InvalidReportInfo { get => _invalidReportInfo && !Loading; set { Set(ref _invalidReportInfo, value); } }


	public DelegateCommand CancelCommand { get; set; }
	public AsyncDelegateCommand CreateReportCommand { get; set; }

	public OpenCsvsWindowViewModel(IViewManager viewManager,
		IMediator mediator)
	{
		CancelCommand = new DelegateCommand(Cancel);
		CreateReportCommand = new AsyncDelegateCommand(CreateReport, CanCreateReport);
		_viewManager = viewManager;
		_mediator = mediator;
	}

	private bool CanCreateReport(object arg)
	{
		return !string.IsNullOrWhiteSpace(IntervalsCsvLocation);
	}

	private async Task CreateReport(object obj)
	{
		try
		{
			Loading = true;
			InvalidReportInfo = false;

			await _mediator.Send(new ReadCsvs(IntervalsCsvLocation));

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
