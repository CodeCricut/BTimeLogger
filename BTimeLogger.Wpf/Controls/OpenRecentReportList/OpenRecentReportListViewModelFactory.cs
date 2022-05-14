using BTimeLogger.Wpf.Services.AppData;
using MediatR;

namespace BTimeLogger.Wpf.Controls;

public interface IOpenRecentReportListViewModelFactory
{
	OpenRecentReportListViewModel Create();
}

class OpenRecentReportListViewModelFactory : IOpenRecentReportListViewModelFactory
{
	private readonly IEventAggregator _ea;
	private readonly IMediator _mediator;
	private readonly IReportLocationsPrincipal _reportLocationsPrincipal;
	private readonly IRecentReportListItemViewModelFactory _recentReportListItemViewModelFactory;

	public OpenRecentReportListViewModelFactory(IEventAggregator ea,
		IMediator mediator,
		IReportLocationsPrincipal reportLocationsPrincipal,
		IRecentReportListItemViewModelFactory recentReportListItemViewModelFactory)
	{
		_ea = ea;
		_mediator = mediator;
		_reportLocationsPrincipal = reportLocationsPrincipal;
		_recentReportListItemViewModelFactory = recentReportListItemViewModelFactory;
	}

	public OpenRecentReportListViewModel Create()
	{
		return new OpenRecentReportListViewModel(_ea, _mediator, _reportLocationsPrincipal, _recentReportListItemViewModelFactory);
	}
}
