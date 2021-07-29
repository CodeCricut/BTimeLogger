using BTimeLogger.Wpf.ViewModels.Factories;
using BTimeLogger.Wpf.ViewModels.MainWindow;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class MainLayoutViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;
		private readonly IOpenCsvsWindowViewModelFactory _createReportWindowViewModelFactory;

		private readonly HomeViewModel _homeViewModel;
		private readonly IntervalsViewModel _intervalsViewModel;
		private readonly StatisticsViewModel _statisticsViewModel;


		public DelegateCommand SelectHomeCommand { get; set; }
		public DelegateCommand SelectIntervalsCommand { get; set; }
		public DelegateCommand SelectStatisticsCommand { get; set; }


		// TODO: create menu vm
		public DelegateCommand CreateReportCommand { get; set; }

		private BaseViewModel _selectedFullscreenViewModel;

		public BaseViewModel SelectedFullscreenViewModel
		{
			get => _selectedFullscreenViewModel;
			set => Set(ref _selectedFullscreenViewModel, value);
		}

		public MainLayoutViewModel(HomeViewModel homeViewModel,
			IntervalsViewModel intervalsViewModel,
			StatisticsViewModel statisticsViewModel,
			IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory createReportWindowViewModelFactory)
		{
			_homeViewModel = homeViewModel;
			_intervalsViewModel = intervalsViewModel;
			_statisticsViewModel = statisticsViewModel;
			_viewManager = viewManager;
			_createReportWindowViewModelFactory = createReportWindowViewModelFactory;
			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectIntervalsCommand = new DelegateCommand(SelectIntervals);
			SelectStatisticsCommand = new DelegateCommand(SelectStatistics);

			CreateReportCommand = new DelegateCommand(CreateReport);
		}

		private void CreateReport(object obj)
		{
			OpenCsvsWindowViewModel reportWindow = _createReportWindowViewModelFactory.Create();
			_viewManager.ShowDialog(reportWindow);
		}

		private void SelectHome(object obj)
		{
			SelectedFullscreenViewModel = _homeViewModel;
		}

		private void SelectIntervals(object obj)
		{
			SelectedFullscreenViewModel = _intervalsViewModel;
		}

		private void SelectStatistics(object obj)
		{
			SelectedFullscreenViewModel = _statisticsViewModel;
		}
	}
}
