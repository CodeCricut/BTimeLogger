using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Controls
{
	public interface IOpenRecentReportListViewModelFactory
	{
		OpenRecentReportListViewModel Create();
	}

	class OpenRecentReportListViewModelFactory : IOpenRecentReportListViewModelFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IMediator _mediator;
		private readonly IReportLocationsPrincipal _reportLocationsPrincipal;

		public OpenRecentReportListViewModelFactory(IEventAggregator ea,
			IMediator mediator,
			IReportLocationsPrincipal reportLocationsPrincipal)
		{
			_ea = ea;
			_mediator = mediator;
			_reportLocationsPrincipal = reportLocationsPrincipal;
		}

		public OpenRecentReportListViewModel Create()
		{
			return new OpenRecentReportListViewModel(_ea, _mediator, _reportLocationsPrincipal);
		}
	}
}
