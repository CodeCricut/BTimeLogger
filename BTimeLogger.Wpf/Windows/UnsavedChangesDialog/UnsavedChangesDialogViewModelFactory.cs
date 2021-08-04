using MediatR;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Windows
{
	public interface IUnsavedChangesDialogViewModelFactory
	{
		UnsavedChangesDialogViewModel Create();
	}

	class UnsavedChangesDialogViewModelFactory : IUnsavedChangesDialogViewModelFactory
	{
		private readonly IMediator _mediator;
		private readonly IViewManager _viewManager;

		public UnsavedChangesDialogViewModelFactory(IMediator mediator,
			IViewManager viewManager)
		{
			_mediator = mediator;
			_viewManager = viewManager;
		}

		public UnsavedChangesDialogViewModel Create()
		{
			return new UnsavedChangesDialogViewModel(_mediator, _viewManager);
		}
	}
}
