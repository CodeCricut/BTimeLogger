using WpfCore.Services;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface ICreateReportWindowViewModelFactory
	{
		CreateReportWindowViewModel Create();
	}

	class CreateReportWindowViewModelFactory : ICreateReportWindowViewModelFactory
	{
		private readonly IViewManager _viewManager;

		public CreateReportWindowViewModelFactory(IViewManager viewManager)
		{
			_viewManager = viewManager;
		}

		public CreateReportWindowViewModel Create()
		{
			return new(_viewManager);
		}
	}
}
