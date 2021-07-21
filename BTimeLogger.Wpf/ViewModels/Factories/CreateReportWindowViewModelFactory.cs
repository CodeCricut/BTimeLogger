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
		private readonly ICsvPrincipal _csvPrincipal;
		private readonly IEventAggregator _ea;

		public CreateReportWindowViewModelFactory(IViewManager viewManager,
			ICsvPrincipal csvPrincipal,
			IEventAggregator ea)
		{
			_viewManager = viewManager;
			_csvPrincipal = csvPrincipal;
			_ea = ea;
		}

		public CreateReportWindowViewModel Create()
		{
			return new CreateReportWindowViewModel(_viewManager, _csvPrincipal, _ea);
		}
	}
}
