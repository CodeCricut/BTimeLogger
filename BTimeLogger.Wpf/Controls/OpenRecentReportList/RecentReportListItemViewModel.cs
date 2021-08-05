using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.MessageBus;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class RecentReportListItemViewModel : BaseViewModel
	{
		private readonly IMediator _mediator;
		private readonly IEventAggregator _ea;
		private readonly IReportLocationsPrincipal _reportLocationsPrincipal;
		private string _reportLocation;
		public string ReportLocation
		{
			get => _reportLocation;
			init => Set(ref _reportLocation, value);
		}


		public AsyncDelegateCommand OpenReportCommand { get; }
		public AsyncDelegateCommand RemoveReportCommand { get; }

		public RecentReportListItemViewModel(string reportLocation,
			IMediator mediator,
			IEventAggregator ea,
			IReportLocationsPrincipal reportLocationsPrincipal)
		{
			if (string.IsNullOrWhiteSpace(reportLocation)) throw new ArgumentException("Report location cannot be empty", nameof(reportLocation));

			ReportLocation = reportLocation;
			_mediator = mediator;
			_ea = ea;

			_reportLocationsPrincipal = reportLocationsPrincipal;

			OpenReportCommand = new AsyncDelegateCommand(OpenReport);
			RemoveReportCommand = new AsyncDelegateCommand(RemoveReport);
		}

		private async Task OpenReport(object arg)
		{
			try
			{
				await _mediator.Send(new ReadCsvs(ReportLocation));
			}
			catch (FileNotFoundException)
			{
				await RemoveReport();
			}
			catch (ReportLocationsDataFileNotFoundException)
			{
				await RemoveReport();
			}
		}

		public Task RemoveReport(object arg = null)
		{
			_reportLocationsPrincipal.RemoveReportLocation(ReportLocation);
			_ea.SendMessage(new ReportSourceChanged());

			return Task.CompletedTask;
		}
	}
}
