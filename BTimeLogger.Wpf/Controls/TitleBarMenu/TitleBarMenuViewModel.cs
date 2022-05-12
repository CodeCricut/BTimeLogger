﻿using BTimeLogger.Wpf.Mediator;
using BTimeLogger.Wpf.Services;
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
		private readonly IOpenRecentCsvsWindowViewModelFactory _openRecentCsvsWindowViewModelFactory;
		private readonly ISaveAsWindowViewModelFactory _saveAsWindowViewModelFactory;
		private readonly IMediator _mediator;
		private readonly ISkinManager _skinManager;

		public DelegateCommand OpenCsvsCommand { get; }
		public DelegateCommand OpenRecentCsvsCommand { get; }
		public AsyncDelegateCommand ExitCommand { get; }
		public AsyncDelegateCommand SaveCommand { get; }
		public AsyncDelegateCommand SaveAsCommand { get; }

		private bool _hasDarkSkinEnabled;
		public bool HasDarkSkinEnabled { get => _hasDarkSkinEnabled; set => Set(ref _hasDarkSkinEnabled, value); }

		public DelegateCommand ToggleSkinCommand { get; }

		public TitleBarMenuViewModel(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory createReportWindowViewModelFactory,
			IOpenRecentCsvsWindowViewModelFactory openRecentCsvsWindowViewModelFactory,
			ISaveAsWindowViewModelFactory saveAsWindowViewModelFactory,
			IMediator mediator,
			ISkinManager skinManager)
		{
			_viewManager = viewManager;
			_createReportWindowViewModelFactory = createReportWindowViewModelFactory;
			_openRecentCsvsWindowViewModelFactory = openRecentCsvsWindowViewModelFactory;
			_saveAsWindowViewModelFactory = saveAsWindowViewModelFactory;
			_mediator = mediator;
			_skinManager = skinManager;

			OpenCsvsCommand = new DelegateCommand(OpenCsvs);
			OpenRecentCsvsCommand = new DelegateCommand(OpenRecentCsvs);
			ExitCommand = new AsyncDelegateCommand(Exit);
			SaveCommand = new AsyncDelegateCommand(Save);
			SaveAsCommand = new AsyncDelegateCommand(SaveAs);

			ToggleSkinCommand = new DelegateCommand(ToggleSkin);
			HasDarkSkinEnabled = _skinManager.AppSkin == Model.Skin.Dark;
		}

		private void ToggleSkin(object obj)
		{
			_skinManager.ToggleSkin();
			HasDarkSkinEnabled = _skinManager.AppSkin == Model.Skin.Dark;
		}

		private void OpenCsvs(object obj)
		{
			OpenCsvsWindowViewModel reportWindow = _createReportWindowViewModelFactory.Create();
			_viewManager.ShowDialog(reportWindow);
		}

		private void OpenRecentCsvs(object obj)
		{
			OpenRecentCsvsWindowViewModel reportWindow = _openRecentCsvsWindowViewModelFactory.Create();
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
	}
}
