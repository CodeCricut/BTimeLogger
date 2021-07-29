using BTimeLogger.Wpf.ViewModels.Factories;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.MainWindow
{
	public class TitleBarMenuViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;
		private readonly IOpenCsvsWindowViewModelFactory _createReportWindowViewModelFactory;

		public DelegateCommand OpenCsvsCommand { get; set; }

		public TitleBarMenuViewModel(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory createReportWindowViewModelFactory)
		{
			_viewManager = viewManager;
			_createReportWindowViewModelFactory = createReportWindowViewModelFactory;

			OpenCsvsCommand = new DelegateCommand(OpenCsvs);
		}

		private void OpenCsvs(object obj)
		{
			OpenCsvsWindowViewModel reportWindow = _createReportWindowViewModelFactory.Create();
			_viewManager.ShowDialog(reportWindow);
		}
	}
}
