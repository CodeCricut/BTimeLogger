using BTimeLogger.Csv;
using BTimeLogger.Domain;
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
		private readonly IActivityReporter _activityReporter;
		private readonly ICsvPrincipal _csvPrincipal;
		private readonly IEventAggregator _ea;

		public CreateReportWindowViewModelFactory(IViewManager viewManager,
			IActivityReporter activityReporter,
			ICsvPrincipal csvPrincipal,
			IEventAggregator ea)
		{
			_viewManager = viewManager;
			_activityReporter = activityReporter;
			_csvPrincipal = csvPrincipal;
			_ea = ea;
		}

		public CreateReportWindowViewModel Create()
		{
			return new CreateReportWindowViewModel(_viewManager, _activityReporter, _csvPrincipal, _ea);
		}
	}
}
