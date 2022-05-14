namespace BTimeLogger.Wpf.Controls;

public interface IWindowButtonsViewModelFactory
{
	WindowButtonsViewModel Create();
}

class WindowButtonsViewModelFactory : IWindowButtonsViewModelFactory
{
	public WindowButtonsViewModel Create()
	{
		return new();
	}
}
