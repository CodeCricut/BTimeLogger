namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IMainWindowViewModelFactory
	{
		MainWindowViewModel Create();
	}

	class MainWindowViewModelFactory : IMainWindowViewModelFactory
	{
		public MainWindowViewModel Create()
		{
			return new();
		}
	}
}
