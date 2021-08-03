namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface ITimeSpanPanelViewModelFactory
	{
		TimeSpanPanelViewModel Create();
	}

	class TimeSpanPanelViewModelFactory : ITimeSpanPanelViewModelFactory
	{
		public TimeSpanPanelViewModel Create()
		{
			return new TimeSpanPanelViewModel();
		}
	}
}
