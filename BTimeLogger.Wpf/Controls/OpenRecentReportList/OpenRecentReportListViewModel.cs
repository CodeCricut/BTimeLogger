using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Controls;

public class OpenRecentReportListViewModel : BaseViewModel
{
	private readonly IEventAggregator _ea;
	private readonly IMediator _mediator;
	private readonly IReportLocationsPrincipal _reportLocationsPrincipal;
	private readonly IRecentReportListItemViewModelFactory _recentReportListItemViewModelFactory;

	public ObservableCollection<RecentReportListItemViewModel> ReportLocations { get; } = new();

	public DelegateCommand ReloadCommand { get; }

	public AsyncDelegateCommand ClearReportsCommand { get; }

	public OpenRecentReportListViewModel(IEventAggregator ea,
		IMediator mediator,
		IReportLocationsPrincipal reportLocationsPrincipal,
		IRecentReportListItemViewModelFactory recentReportListItemViewModelFactory)
	{
		_ea = ea;
		_mediator = mediator;
		_reportLocationsPrincipal = reportLocationsPrincipal;
		_recentReportListItemViewModelFactory = recentReportListItemViewModelFactory;
		ReloadCommand = new DelegateCommand(Reload);
		ClearReportsCommand = new AsyncDelegateCommand(ClearReports);

		ea.RegisterHandler<ReportSourceChanged>(msg => ReloadCommand.Execute());

		ReloadCommand.Execute();
	}

	private Task ClearReports(object reportLocation)
	{
		ReportLocations.Clear();
		_reportLocationsPrincipal.ClearReportLocations();
		_ea.SendMessage(new ReportSourceChanged());

		return Task.CompletedTask;
	}

	private void Reload(object _)
	{
		ReportLocations.Clear();

		IEnumerable<string> reportLocations = _reportLocationsPrincipal.GetReportLocations();
		foreach (string location in reportLocations)
		{
			RecentReportListItemViewModel listItemVM = _recentReportListItemViewModelFactory.Create(location);
			ReportLocations.Add(listItemVM);
		}
	}
}
