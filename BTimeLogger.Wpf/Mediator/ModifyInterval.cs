using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Controls;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Mediator
{
	public class ModifyInterval : IRequest
	{
		public ModifyInterval(Interval interval)
		{
			Interval = interval;
		}

		public Interval Interval { get; }
	}

	public class ModifyIntervalHandler : IRequestHandler<ModifyInterval>
	{
		private readonly IIntervalRepository _intervalRepository;
		private readonly IEventAggregator _ea;

		public ModifyIntervalHandler(IIntervalRepository intervalRepository,
			IEventAggregator ea)
		{
			_intervalRepository = intervalRepository;
			_ea = ea;
		}

		public async Task<Unit> Handle(ModifyInterval request, CancellationToken cancellationToken)
		{
			// TODO: cancellation token
			Interval intervalToUpdate = request.Interval;
			await _intervalRepository.UpdateInterval(intervalToUpdate.Guid, intervalToUpdate);

			_ea.SendMessage(new ReportSourceChanged());

			return Unit.Value;
		}
	}
}
