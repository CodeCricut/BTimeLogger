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

		public SaveHandler(
			ICsvLocationPrincipal csvLocationPrincipal,
			IIntervalsCsvWriter intervalsCsvWriter)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_intervalsCsvWriter = intervalsCsvWriter;
		}

		public async Task<Unit> Handle(Save request, CancellationToken cancellationToken)
		{
			if (_csvLocationPrincipal.LocationsAreSelected)
			{
				await _intervalsCsvWriter.WriteIntervals(_csvLocationPrincipal.IntervalCsvLocation);
			}
			// TODO: if save invoked without file location selected, prompt user for file location / invoke save as
			return Unit.Value;
		}
	}
}
