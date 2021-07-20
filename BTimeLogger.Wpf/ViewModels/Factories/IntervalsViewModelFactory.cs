using BTimeLogger.Wpf.ViewModels.MainWindow;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IIntervalsViewModelFactory
	{
		IntervalsViewModel Create();
	}

	class IntervalsViewModelFactory : IIntervalsViewModelFactory
	{
		public IntervalsViewModel Create()
		{
			return new();
		}
	}
}
