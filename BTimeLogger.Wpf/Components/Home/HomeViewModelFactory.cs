using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IHomeViewModelFactory
	{
		HomeViewModel Create();
	}

	class HomeViewModelFactory : IHomeViewModelFactory
	{
		public HomeViewModel Create()
		{
			return new();
		}
	}
}
