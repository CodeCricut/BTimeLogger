using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator;

public class Shutdown : IRequest<bool>
{
}

public class ShutdownHandler : IRequestHandler<Shutdown, bool>
{
	private readonly IMediator _mediator;

	public ShutdownHandler(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task<bool> Handle(Shutdown request, CancellationToken cancellationToken)
	{
		// TODO Issue #9: Ask user if they would like to save before exiting

		App.Current.Shutdown();
		return Task.FromResult(true);
	}
}
