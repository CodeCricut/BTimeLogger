using BTimeLogger.Wpf.ViewModels.MainWindow;
using WpfCore.Services;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface ITitleBarMenuViewModelFactory
	{
		TitleBarMenuViewModel Create();
	}

	class TitleBarMenuViewModelFactory : ITitleBarMenuViewModelFactory
	{
		private readonly IViewManager _viewManager;
		private readonly IOpenCsvsWindowViewModelFactory _openCsvsWindowViewModelFactory;

		public TitleBarMenuViewModelFactory(IViewManager viewManager,
			IOpenCsvsWindowViewModelFactory openCsvsWindowViewModelFactory)
		{
			_viewManager = viewManager;
			_openCsvsWindowViewModelFactory = openCsvsWindowViewModelFactory;
		}

		public TitleBarMenuViewModel Create()
		{
			return new TitleBarMenuViewModel(_viewManager, _openCsvsWindowViewModelFactory);
		}
	}
}
