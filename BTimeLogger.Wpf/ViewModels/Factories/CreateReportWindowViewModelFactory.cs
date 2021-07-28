using BTimeLogger.Csv;
using WpfCore.MessageBus;
using WpfCore.Services;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface ICreateReportWindowViewModelFactory
	{
		CreateReportWindowViewModel Create();
	}

	class CreateReportWindowViewModelFactory : ICreateReportWindowViewModelFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IIntervalsCsvReader _intervalsCsvReader;
		private readonly IStatisticsCsvReader _statisticsCsvReader;
		private readonly IEventAggregator _ea;

		public CreateReportWindowViewModelFactory(IViewManager viewManager,
			IIntervalsCsvReader intervalsCsvReader,
			IStatisticsCsvReader statisticsCsvReader,
			IEventAggregator ea)
		{
			_viewManager = viewManager;
			_intervalsCsvReader = intervalsCsvReader;
			_statisticsCsvReader = statisticsCsvReader;
			_ea = ea;
		}

		public CreateReportWindowViewModel Create()
		{
			return new CreateReportWindowViewModel(_viewManager, _intervalsCsvReader, _statisticsCsvReader, _ea);
		}
	}
}
