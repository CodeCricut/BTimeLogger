using BTimeLogger.Wpf.Windows;
using MediatR;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class HomeViewModel : BaseViewModel
	{
		private readonly IMediator _mediator;
		private readonly ICreateNewProjectWindowViewModelFactory _createNewProjectWindowViewModelFactory;
		private readonly IViewManager _viewManager;

		public DelegateCommand CreateNewProjectCommand { get; }

		public HomeViewModel(OpenRecentReportListViewModel openRecentReportListViewModel,
			ICreateNewProjectWindowViewModelFactory createNewProjectWindowViewModelFactory,
			IViewManager viewManager)
		{
			OpenRecentReportListViewModel = openRecentReportListViewModel;
			_createNewProjectWindowViewModelFactory = createNewProjectWindowViewModelFactory;
			_viewManager = viewManager;

			CreateNewProjectCommand = new DelegateCommand(CreateNewProject);
		}

		private void CreateNewProject(object obj)
		{
			CreateNewProjectWindowViewModel newProjectVM = _createNewProjectWindowViewModelFactory.Create();
			_viewManager.ShowDialog(newProjectVM);
		}

		public OpenRecentReportListViewModel OpenRecentReportListViewModel { get; }
	}
}
