using BTimeLogger.Wpf.Controls;

namespace BTimeLogger.Wpf.Windows;

public class ModifyIntervalWindowViewModel : BaseViewModel
{
	public ModifyIntervalWindowViewModel(ModifyIntervalViewModel modifyIntervalViewModel,
		WindowButtonsViewModel windowButtonsViewModel)
	{
		ModifyIntervalViewModel = modifyIntervalViewModel;
		WindowButtonsViewModel = windowButtonsViewModel;

		ModifyIntervalViewModel.InteractionFinished += (_, _) => WindowButtonsViewModel.CloseCommand.Execute();
	}

	public ModifyIntervalViewModel ModifyIntervalViewModel { get; }
	public WindowButtonsViewModel WindowButtonsViewModel { get; }
}
