using BTimeLogger.Wpf.Controls;
using MediatR;
using System;

namespace BTimeLogger.Wpf.Windows;

public class MainWindowViewModel : BaseViewModel
{
	private readonly IViewManager _viewManager;
	private readonly IMediator _mediator;

	public WindowButtonsViewModel WindowButtonsViewModel { get; set; }
	public MainLayoutViewModel MainLayoutViewModel { get; }
	public TitleBarMenuViewModel TitleBarMenuViewModel { get; }

	public MainWindowViewModel(
		WindowButtonsViewModel windowButtonsViewModel,
		MainLayoutViewModel mainLayoutViewModel,
		TitleBarMenuViewModel titleBarMenuViewModel,
		IMediator mediator,
		IViewManager viewManager)
	{
		WindowButtonsViewModel = windowButtonsViewModel;
		MainLayoutViewModel = mainLayoutViewModel;
		TitleBarMenuViewModel = titleBarMenuViewModel;

		_mediator = mediator;
		_viewManager = viewManager;

		WindowButtonsViewModel.Closed += HandleCloseRequested;
	}

	private void HandleCloseRequested(object sender, EventArgs e)
	{
		bool? saved = _mediator.Send(new Mediator.PromptToSaveUnsavedChanges())
			.GetAwaiter().GetResult();

		if (saved.HasValue && saved.Value) // Not cancelled
		{
			_viewManager.Close(this);
		}
	}
}
