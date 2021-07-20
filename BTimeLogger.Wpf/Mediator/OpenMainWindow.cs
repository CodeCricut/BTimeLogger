using BTimeLogger.Wpf.ViewModels;
using BTimeLogger.Wpf.ViewModels.Factories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Mediator
{
	public class OpenMainWindowRequest : IRequest
	{
		public OpenMainWindowRequest()
		{

		}
	}

	public class OpenMainWindowHandler : IRequestHandler<OpenMainWindowRequest>
	{
		private readonly IMainWindowViewModelFactory _mainWindowViewModelFactory;
		private readonly IViewManager _viewManager;

		public OpenMainWindowHandler(IMainWindowViewModelFactory mainWindowViewModelFactory,
			IViewManager viewManager)
		{
			_mainWindowViewModelFactory = mainWindowViewModelFactory;
			_viewManager = viewManager;
		}

		public Task<Unit> Handle(OpenMainWindowRequest request, CancellationToken cancellationToken)
		{
			MainWindowViewModel viewModel = _mainWindowViewModelFactory.Create();
			_viewManager.Show(viewModel);
			return Unit.Task;
		}
	}
}
