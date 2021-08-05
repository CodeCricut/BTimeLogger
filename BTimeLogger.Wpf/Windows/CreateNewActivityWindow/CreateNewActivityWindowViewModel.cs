using BTimeLogger.Wpf.Controls;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Windows
{
	public class CreateNewActivityWindowViewModel : BaseViewModel
	{
		public CreateNewActivityWindowViewModel(WindowButtonsViewModel windowButtonsViewModel,
			CreateNewActivityViewModel createNewActivityViewModel)
		{
			WindowButtonsViewModel = windowButtonsViewModel;
			CreateNewActivityViewModel = createNewActivityViewModel;

			CreateNewActivityViewModel.InteractionFinished += (_, _) => WindowButtonsViewModel.CloseCommand.Execute();
		}

		public WindowButtonsViewModel WindowButtonsViewModel { get; }
		public CreateNewActivityViewModel CreateNewActivityViewModel { get; }
	}
}
