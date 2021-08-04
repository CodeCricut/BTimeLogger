using BTimeLogger.Csv;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Controls;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.Mediator
{
	public class CreateNewInterval : IRequest<Guid>
	{
		public CreateNewInterval(Interval interval)
		{
			Interval = interval;
		}

		public Interval Interval { get; }
	}

	public class CreateNewIntervalHandler : IRequestHandler<CreateNewInterval, Guid>
	{
		private readonly IIntervalRepository _intervalRepository;
		private readonly IEventAggregator _ea;
		private readonly ICsvChangeTracker _csvChangeTracker;

		public CreateNewIntervalHandler(IIntervalRepository intervalRepository,
			IEventAggregator ea,
			ICsvChangeTracker csvChangeTracker)
		{
			_intervalRepository = intervalRepository;
			_ea = ea;
			_csvChangeTracker = csvChangeTracker;
		}

		public async Task<Guid> Handle(CreateNewInterval request, CancellationToken cancellationToken)
		{
			Guid newIntervalGuid = await _intervalRepository.AddInterval(request.Interval);
			await _intervalRepository.SaveChanges();

			_csvChangeTracker.MakeChange();

			_ea.SendMessage(new ReportSourceChanged());

			return newIntervalGuid;
		}
	}
}
