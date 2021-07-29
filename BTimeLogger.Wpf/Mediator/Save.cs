using BTimeLogger.Csv;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator
{
	public class Save : IRequest
	{
	}

	public class SaveHandler : IRequestHandler<Save>
	{
		private readonly ICsvLocationPrincipal _csvLocationPrincipal;
		private readonly IIntervalsCsvWriter _intervalsCsvWriter;
		private readonly IStatisticsCsvWriter _statisticsCsvWriter;

		public SaveHandler(
			ICsvLocationPrincipal csvLocationPrincipal,
			IIntervalsCsvWriter intervalsCsvWriter,
			IStatisticsCsvWriter statisticsCsvWriter)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_intervalsCsvWriter = intervalsCsvWriter;
			_statisticsCsvWriter = statisticsCsvWriter;
		}

		public async Task<Unit> Handle(Save request, CancellationToken cancellationToken)
		{
			if (_csvLocationPrincipal.LocationsAreSelected)
			{
				await _intervalsCsvWriter.WriteIntervals(_csvLocationPrincipal.IntervalCsvLocation);
				await _statisticsCsvWriter.WriteStatistics(_csvLocationPrincipal.StatisticCsvLocation);
			}
			return Unit.Value;
		}
	}
}
