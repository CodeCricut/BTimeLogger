﻿using BTimeLogger.Wpf.ViewModels.MainWindow;
using WpfCore.Commands;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class MainLayoutViewModel : BaseViewModel
	{
		private readonly HomeViewModel _homeViewModel;
		private readonly IntervalsViewModel _intervalsViewModel;
		private readonly StatisticsViewModel _statisticsViewModel;

		public DelegateCommand SelectHomeCommand { get; set; }
		public DelegateCommand SelectIntervalsCommand { get; set; }
		public DelegateCommand SelectStatisticsCommand { get; set; }

		private BaseViewModel _selectedFullscreenViewModel;

		public BaseViewModel SelectedFullscreenViewModel
		{
			get => _selectedFullscreenViewModel;
			set => Set(ref _selectedFullscreenViewModel, value);
		}

		public MainLayoutViewModel(HomeViewModel homeViewModel,
			IntervalsViewModel intervalsViewModel,
			StatisticsViewModel statisticsViewModel)
		{
			_homeViewModel = homeViewModel;
			_intervalsViewModel = intervalsViewModel;
			_statisticsViewModel = statisticsViewModel;

			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectIntervalsCommand = new DelegateCommand(SelectIntervals);
			SelectStatisticsCommand = new DelegateCommand(SelectStatistics);
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
