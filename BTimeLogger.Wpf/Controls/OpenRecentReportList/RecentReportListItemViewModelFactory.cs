using BTimeLogger.Wpf.Services.AppData;
using MediatR;

namespace BTimeLogger.Wpf.Controls;

public interface IRecentReportListItemViewModelFactory
{
	RecentReportListItemViewModel Create(string reportLocation);
}

class RecentReportListItemViewModelFactory : IRecentReportListItemViewModelFactory
{
	private readonly IMediator _mediator;
	private readonly IEventAggregator _ea;
	private readonly IReportLocationsPrincipal _reportLocationsPrincipal;

	public RecentReportListItemViewModelFactory(IMediator mediator,
		IEventAggregator ea,
		IReportLocationsPrincipal reportLocationsPrincipal)
	{
		_mediator = mediator;
		_ea = ea;
		_reportLocationsPrincipal = reportLocationsPrincipal;
	}

	public RecentReportListItemViewModel Create(string reportLocation)
	{
		return new RecentReportListItemViewModel(reportLocation, _mediator, _ea, _reportLocationsPrincipal);
	}
}
