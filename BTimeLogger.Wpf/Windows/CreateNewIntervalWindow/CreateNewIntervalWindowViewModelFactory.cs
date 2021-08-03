using BTimeLogger.Wpf.Controls;

namespace BTimeLogger.Wpf.Windows
{
	public interface ICreateNewIntervalWindowViewModelFactory
	{
		CreateNewIntervalWindowViewModel Create();
	}
	class CreateNewIntervalWindowViewModelFactory : ICreateNewIntervalWindowViewModelFactory
	{
		private readonly IWindowButtonsViewModelFactory _windowButtonsViewModelFactory;
		private readonly ICreateNewIntervalViewModelFactory _createNewIntervalViewModelFactory;

		public CreateNewIntervalWindowViewModelFactory(IWindowButtonsViewModelFactory windowButtonsViewModelFactory,
			ICreateNewIntervalViewModelFactory createNewIntervalViewModelFactory)
		{
			_windowButtonsViewModelFactory = windowButtonsViewModelFactory;
			_createNewIntervalViewModelFactory = createNewIntervalViewModelFactory;
		}
		public CreateNewIntervalWindowViewModel Create()
		{
			var windowButtonsVM = _windowButtonsViewModelFactory.Create();
			var createNewIntervalVM = _createNewIntervalViewModelFactory.Create();

			return new CreateNewIntervalWindowViewModel(windowButtonsVM, createNewIntervalVM);

		}
	}
}
