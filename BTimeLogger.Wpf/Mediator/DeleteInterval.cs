using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Controls;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Mediator
{
	public class DeleteInterval : IRequest
	{
		public DeleteInterval(Interval interval)
		{
			Interval = interval;
		}

		public Interval Interval { get; }
	}

	public class DeleteIntervalHandler : IRequestHandler<DeleteInterval>
	{
		private readonly IIntervalRepository _intervalRepository;
		private readonly IEventAggregator _ea;

		public DeleteIntervalHandler(IIntervalRepository intervalRepository,
			IEventAggregator ea)
		{
			_intervalRepository = intervalRepository;
			_ea = ea;
		}

		public async Task<Unit> Handle(DeleteInterval request, CancellationToken cancellationToken)
		{
			await _intervalRepository.DeleteInterval(request.Interval.Guid);

			_ea.SendMessage(new ReportSourceChanged());

			return Unit.Value;
		}
	}
}
