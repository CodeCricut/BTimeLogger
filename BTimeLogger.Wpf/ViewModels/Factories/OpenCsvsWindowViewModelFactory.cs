using BTimeLogger.Csv;
using MediatR;
using WpfCore.MessageBus;
using WpfCore.Services;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IOpenCsvsWindowViewModelFactory
	{
		OpenCsvsWindowViewModel Create();
	}

	class OpenCsvsWindowViewModelFactory : IOpenCsvsWindowViewModelFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IIntervalsCsvReader _intervalsCsvReader;
		private readonly IStatisticsCsvReader _statisticsCsvReader;
		private readonly IEventAggregator _ea;
		private readonly IMediator _mediator;

		public OpenCsvsWindowViewModelFactory(
			IViewManager viewManager,
			IIntervalsCsvReader intervalsCsvReader,
			IStatisticsCsvReader statisticsCsvReader,
			IEventAggregator ea,
			IMediator mediator)
		{
			_viewManager = viewManager;
			_intervalsCsvReader = intervalsCsvReader;
			_statisticsCsvReader = statisticsCsvReader;
			_ea = ea;
			_mediator = mediator;
		}

		public OpenCsvsWindowViewModel Create()
		{
			return new OpenCsvsWindowViewModel(_viewManager, _ea, _mediator);
		}
	}
}
