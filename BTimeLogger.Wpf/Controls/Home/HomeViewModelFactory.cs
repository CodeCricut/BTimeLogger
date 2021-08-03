namespace BTimeLogger.Wpf.Controls
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
