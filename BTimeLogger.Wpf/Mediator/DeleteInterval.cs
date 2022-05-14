using BTimeLogger.Csv;
using BTimeLogger.Domain.Services;
using BTimeLogger.Wpf.Controls;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator;

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
	private readonly ICsvChangeTracker _csvChangeTracker;

	public DeleteIntervalHandler(IIntervalRepository intervalRepository,
		IEventAggregator ea,
		ICsvChangeTracker csvChangeTracker)
	{
		_intervalRepository = intervalRepository;
		_ea = ea;
		_csvChangeTracker = csvChangeTracker;
	}

	public async Task<Unit> Handle(DeleteInterval request, CancellationToken cancellationToken)
	{
		await _intervalRepository.DeleteInterval(request.Interval.Guid);
		await _intervalRepository.SaveChanges();
		_csvChangeTracker.MakeChange();

		_ea.SendMessage(new ReportSourceChanged());

		return Unit.Value;
	}
}
