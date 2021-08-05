using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class OpenRecentReportListViewModel : BaseViewModel
	{
		private readonly IMediator _mediator;
		private readonly IReportLocationsPrincipal _reportLocationsPrincipal;

		public ObservableCollection<string> ReportLocations { get; } = new();

		public DelegateCommand ReloadCommand { get; }

		public AsyncDelegateCommand OpenReportCommand { get; }

		public OpenRecentReportListViewModel(IEventAggregator ea,
			IMediator mediator,
			IReportLocationsPrincipal reportLocationsPrincipal)
		{
			_mediator = mediator;
			_reportLocationsPrincipal = reportLocationsPrincipal;

			ReloadCommand = new DelegateCommand(Reload);
			OpenReportCommand = new AsyncDelegateCommand(OpenReport);

			ea.RegisterHandler<ReportSourceChanged>(msg => ReloadCommand.Execute());

			ReloadCommand.Execute();
		}

		private async Task OpenReport(object reportLocation)
		{
			if (reportLocation is not string || string.IsNullOrWhiteSpace((string)reportLocation))
				throw new ArgumentException("Report location cannot be empty.", nameof(reportLocation));

			await _mediator.Send(new ReadCsvs(reportLocation as string));
		}

		private void Reload(object _)
		{
			ReportLocations.Clear();

			IEnumerable<string> reportLocations = _reportLocationsPrincipal.GetReportLocations();
			foreach (string location in reportLocations)
				ReportLocations.Add(location);
		}
	}
}
