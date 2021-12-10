using BTimeLogger.Csv;
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
		private readonly ICsvChangeTracker _csvChangeTracker;

		public ModifyIntervalHandler(IIntervalRepository intervalRepository,
			IEventAggregator ea,
			ICsvChangeTracker csvChangeTracker)
		{
			_intervalRepository = intervalRepository;
			_ea = ea;
			_csvChangeTracker = csvChangeTracker;
		}

		public async Task<Unit> Handle(ModifyInterval request, CancellationToken cancellationToken)
		{
			// TODO Issue #7: Add cancel option to ModifyInterval command
			Interval intervalToUpdate = request.Interval;
			await _intervalRepository.UpdateInterval(intervalToUpdate.Guid, intervalToUpdate);
			await _intervalRepository.SaveChanges();

			_csvChangeTracker.MakeChange();

			_ea.SendMessage(new ReportSourceChanged());

			return Unit.Value;
		}
	}
}
