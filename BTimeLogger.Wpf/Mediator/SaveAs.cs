using BTimeLogger.Csv.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator;

public class SaveAs : IRequest
{
	public SaveAs(string fileLocation)
	{
		if (string.IsNullOrWhiteSpace(fileLocation))
		{
			throw new ArgumentException($"'{nameof(fileLocation)}' cannot be null or whitespace.", nameof(fileLocation));
		}
		FileLocation = fileLocation;
	}

	public string FileLocation { get; }
}

public class SaveAsHandler : IRequestHandler<SaveAs>
{
	private readonly ICsvLocationPrincipal _csvLocationsPrincipal;
	private readonly IMediator _mediator;

	public SaveAsHandler(ICsvLocationPrincipal csvLocationsPrincipal,
		IMediator mediator)
	{
		_csvLocationsPrincipal = csvLocationsPrincipal;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(SaveAs request, CancellationToken cancellationToken)
	{
		_csvLocationsPrincipal.CsvLocation = request.FileLocation;
		await _mediator.Send(new Save(), cancellationToken);
		await _mediator.Send(new ReadCsvs(request.FileLocation));

		return Unit.Value;
	}
}
