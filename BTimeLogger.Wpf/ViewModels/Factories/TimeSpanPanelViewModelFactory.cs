using WpfCore.MessageBus;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface ITimeSpanPanelViewModelFactory
	{
		TimeSpanPanelViewModel Create();
	}

	class TimeSpanPanelViewModelFactory : ITimeSpanPanelViewModelFactory
	{
		private readonly IEventAggregator _ea;

		public TimeSpanPanelViewModelFactory(IEventAggregator ea)
		{
			_ea = ea;
		}

		public TimeSpanPanelViewModel Create()
		{
			return new(_ea);
		}
	}
}
