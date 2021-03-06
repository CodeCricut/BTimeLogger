using BTimeLogger.Wpf.Controls;

namespace BTimeLogger.Wpf.Windows;

public class OpenRecentCsvsWindowViewModel : BaseViewModel
{
	private readonly IViewManager _viewManager;

	public WindowButtonsViewModel WindowButtonsViewModel { get; }
	public OpenRecentReportListViewModel OpenRecentReportListViewModel { get; }


	public DelegateCommand CancelCommand { get; set; }

	public OpenRecentCsvsWindowViewModel(IViewManager viewManager,
									IEventAggregator ea,
								   WindowButtonsViewModel windowButtonsViewModel,
								   OpenRecentReportListViewModel openRecentReportListViewModel)
	{
		_viewManager = viewManager;
		WindowButtonsViewModel = windowButtonsViewModel;
		OpenRecentReportListViewModel = openRecentReportListViewModel;

		CancelCommand = new DelegateCommand(Cancel);

		ea.RegisterHandler<ReportSelected>(msg => CloseDialog());

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
