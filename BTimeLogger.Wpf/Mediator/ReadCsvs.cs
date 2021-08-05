using BTimeLogger.Csv;
using BTimeLogger.Wpf.Controls;
using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Mediator
{
	public class ReadCsvs : IRequest
	{
		public ReadCsvs(string intervalCsvLocation)
		{
			IntervalCsvLocation = intervalCsvLocation;
		}

		public string IntervalCsvLocation { get; }
		public string StatisticsCsvLocation { get; }
	}

	public class ReadCsvsHandler : IRequestHandler<ReadCsvs>
	{
		private readonly ICsvLocationPrincipal _csvLocationPrincipal;
		private readonly IIntervalsCsvReader _intervalsCsvReader;
		private readonly IMediator _mediator;
		private readonly ICsvChangeTracker _csvChangeTracker;
		private readonly IReportLocationsPrincipal _reportLocationsPrincipal;
		private readonly IEventAggregator _ea;

		public ReadCsvsHandler(
			ICsvLocationPrincipal csvLocationPrincipal,
			IIntervalsCsvReader intervalsCsvReader,
			IMediator mediator,
			ICsvChangeTracker csvChangeTracker,
			IReportLocationsPrincipal reportLocationsPrincipal,
			IEventAggregator ea)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_intervalsCsvReader = intervalsCsvReader;
			_mediator = mediator;
			_csvChangeTracker = csvChangeTracker;
			_reportLocationsPrincipal = reportLocationsPrincipal;
			_ea = ea;
		}

		public async Task<Unit> Handle(ReadCsvs request, CancellationToken cancellationToken)
		{
			if (_csvChangeTracker.ChangesMade)
			{
				await _mediator.Send(new Save());
			}

			await _mediator.Send(new ClearAllData());


			// TODO: use cancellation token, with csvreaders having SaveChanges function
			_csvLocationPrincipal.IntervalCsvLocation = request.IntervalCsvLocation;
			_reportLocationsPrincipal.AddReportLocation(request.IntervalCsvLocation);

			await _intervalsCsvReader.ReadIntervalCsv(request.IntervalCsvLocation);

			_csvChangeTracker.ClearChanges();

			_ea.SendMessage(new ReportSourceChanged());

			return Unit.Value;
		}
	}
}
