using BTimeLogger.Csv;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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

		public ReadCsvsHandler(
			ICsvLocationPrincipal csvLocationPrincipal,
			IIntervalsCsvReader intervalsCsvReader,
			IMediator mediator)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_intervalsCsvReader = intervalsCsvReader;
			_mediator = mediator;
		}

		public async Task<Unit> Handle(ReadCsvs request, CancellationToken cancellationToken)
		{
			// TODO: use cancellation token, with csvreaders having SaveChanges function

			await _mediator.Send(new ClearAllData());

			_csvLocationPrincipal.IntervalCsvLocation = request.IntervalCsvLocation;

			await _intervalsCsvReader.ReadIntervalCsv(request.IntervalCsvLocation);

			return Unit.Value;
		}
	}
}
