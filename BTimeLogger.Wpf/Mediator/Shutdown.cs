using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator
{
	public class Shutdown : IRequest<bool>
	{
	}

	public class ShutdownHandler : IRequestHandler<Shutdown, bool>
	{
		public ShutdownHandler()
		{

		}

		public Task<bool> Handle(Shutdown request, CancellationToken cancellationToken)
		{
			App.Current.Shutdown();
			return Task.FromResult(true);
		}
	}
}
