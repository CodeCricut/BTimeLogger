using BTimeLogger.Wpf.Controls;

namespace BTimeLogger.Wpf.Windows;

public interface ICreateNewActivityWindowViewModelFactory
{
	CreateNewActivityWindowViewModel Create();
}

class CreateNewActivityWindowViewModelFactory : ICreateNewActivityWindowViewModelFactory
{
	private readonly IWindowButtonsViewModelFactory _windowButtonsViewModel;
	private readonly ICreateNewActivityViewModelFactory _createNewActivityViewModelFactory;

	public CreateNewActivityWindowViewModelFactory(IWindowButtonsViewModelFactory windowButtonsViewModel,
		ICreateNewActivityViewModelFactory createNewActivityViewModelFactory)
	{
		_windowButtonsViewModel = windowButtonsViewModel;
		_createNewActivityViewModelFactory = createNewActivityViewModelFactory;
	}

	public CreateNewActivityWindowViewModel Create()
	{
		WindowButtonsViewModel windowsButtonsVM = _windowButtonsViewModel.Create();
		CreateNewActivityViewModel createNewActivityVM = _createNewActivityViewModelFactory.Create();

		return new CreateNewActivityWindowViewModel(windowsButtonsVM, createNewActivityVM);
	}
}
