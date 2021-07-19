namespace BTimeLogger.Wpf.ViewModels.Factories
{
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
}
