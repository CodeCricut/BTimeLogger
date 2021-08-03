using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class CreateNewIntervalWindowViewModel : BaseViewModel
	{
		public CreateNewIntervalWindowViewModel(WindowButtonsViewModel windowButtonsViewModel,
			CreateNewIntervalViewModel createNewIntervalViewModel)
		{
			WindowButtonsViewModel = windowButtonsViewModel;
			CreateNewIntervalViewModel = createNewIntervalViewModel;

			CreateNewIntervalViewModel.InteractionFinished += (_, _) => WindowButtonsViewModel.CloseCommand.Execute();
		}

		public WindowButtonsViewModel WindowButtonsViewModel { get; }
		public CreateNewIntervalViewModel CreateNewIntervalViewModel { get; }
	}
}
