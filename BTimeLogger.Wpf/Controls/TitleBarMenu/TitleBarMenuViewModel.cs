using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.Windows;
using MediatR;
using System.Threading.Tasks;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class TitleBarMenuViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;
		private readonly IOpenCsvsWindowViewModelFactory _createReportWindowViewModelFactory;
		private readonly ICreateNewIntervalWindowViewModelFactory _createNewIntervalWindowViewModelFactory;
		private readonly ICreateNewActivityWindowViewModelFactory _createNewActivityWindowViewModelFactory;
		private readonly ISaveAsWindowViewModelFactory _saveAsWindowViewModelFactory;
		private readonly IMediator _mediator;

		public DelegateCommand OpenCsvsCommand { get; }
		public AsyncDelegateCommand ExitCommand { get; }
		public AsyncDelegateCommand SaveCommand { get; }
		public AsyncDelegateCommand SaveAsCommand { get; }

		public AsyncDelegateCommand CreateIntervalCommand { get; }
		public AsyncDelegateCommand CreateActivityCommand { get; }

		public TitleBarMenuViewModel(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory createReportWindowViewModelFactory,
			ICreateNewIntervalWindowViewModelFactory createNewIntervalWindowViewModelFactory,
			ICreateNewActivityWindowViewModelFactory createNewActivityWindowViewModelFactory,
			ISaveAsWindowViewModelFactory saveAsWindowViewModelFactory,
			IMediator mediator)
		{
			_viewManager = viewManager;
			_createReportWindowViewModelFactory = createReportWindowViewModelFactory;
			_createNewIntervalWindowViewModelFactory = createNewIntervalWindowViewModelFactory;
			_createNewActivityWindowViewModelFactory = createNewActivityWindowViewModelFactory;
			_saveAsWindowViewModelFactory = saveAsWindowViewModelFactory;
			_mediator = mediator;

			OpenCsvsCommand = new DelegateCommand(OpenCsvs);
			ExitCommand = new AsyncDelegateCommand(Exit);
			SaveCommand = new AsyncDelegateCommand(Save);
			SaveAsCommand = new AsyncDelegateCommand(SaveAs);

			CreateIntervalCommand = new AsyncDelegateCommand(CreateNewInterval);
			CreateActivityCommand = new AsyncDelegateCommand(CreateNewActivity);
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

		private Task SaveAs(object _)
		{
			SaveAsWindowViewModel vm = _saveAsWindowViewModelFactory.Create();
			_viewManager.ShowDialog(vm);
			return Task.CompletedTask;
		}

		private Task CreateNewInterval(object arg)
		{
			CreateNewIntervalWindowViewModel createIntervalWindowVM = _createNewIntervalWindowViewModelFactory.Create();
			_viewManager.ShowDialog(createIntervalWindowVM);

			return Task.CompletedTask;
		}

		private Task CreateNewActivity(object arg)
		{
			CreateNewActivityWindowViewModel createActivityWindowVM = _createNewActivityWindowViewModelFactory.Create();
			_viewManager.ShowDialog(createActivityWindowVM);

			return Task.CompletedTask;
		}
	}
}
