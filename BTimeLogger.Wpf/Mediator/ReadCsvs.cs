using BTimeLogger.Csv;
using BTimeLogger.Csv.Services;
using BTimeLogger.Wpf.Controls;
using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator;

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
		// TODO Issue #8: Add cancel option to ReadCsvs command
		bool continueReading = true;
		if (_csvChangeTracker.ChangesMade)
		{
			continueReading = await _mediator.Send(new PromptToSaveUnsavedChanges()) ?? false;
		}

		if (continueReading)
		{
			await ReadCsv(request);
		}

		return Unit.Value;
	}

	private async Task ReadCsv(ReadCsvs request)
	{
		await _mediator.Send(new ClearAllData());

		_csvLocationPrincipal.CsvLocation = request.IntervalCsvLocation;
		_reportLocationsPrincipal.AddReportLocation(request.IntervalCsvLocation);

		await _intervalsCsvReader.ReadIntervalCsv(request.IntervalCsvLocation);

		_csvChangeTracker.ClearChanges();

		_ea.SendMessage(new ReportSourceChanged());
	}
}
