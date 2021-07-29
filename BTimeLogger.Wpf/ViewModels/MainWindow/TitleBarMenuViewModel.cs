using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.ViewModels.Factories;
using MediatR;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class TitleBarMenuViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;
		private readonly IOpenCsvsWindowViewModelFactory _createReportWindowViewModelFactory;
		private readonly IMediator _mediator;

		public DelegateCommand OpenCsvsCommand { get; }
		public AsyncDelegateCommand ExitCommand { get; }
		public AsyncDelegateCommand SaveCommand { get; }

		public TitleBarMenuViewModel(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory createReportWindowViewModelFactory,
			IMediator mediator)
		{
			_viewManager = viewManager;
			_createReportWindowViewModelFactory = createReportWindowViewModelFactory;
			_mediator = mediator;

			OpenCsvsCommand = new DelegateCommand(OpenCsvs);
			ExitCommand = new AsyncDelegateCommand(Exit);
			SaveCommand = new AsyncDelegateCommand(Save);
		}

		private void OpenCsvs(object obj)
		{
			OpenCsvsWindowViewModel reportWindow = _createReportWindowViewModelFactory.Create();
			_viewManager.ShowDialog(reportWindow);
		}

		private Task Exit(object obj)
		{
			return _mediator.Send(new Shutdown());
		}

		private Task Save(object _)
		{
			return _mediator.Send(new Save());
		}
	}
}
