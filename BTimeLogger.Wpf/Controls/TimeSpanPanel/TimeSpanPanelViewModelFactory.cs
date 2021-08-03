namespace BTimeLogger.Wpf.Controls
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
