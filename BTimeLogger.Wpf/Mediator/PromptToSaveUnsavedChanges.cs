using BTimeLogger.Csv;
using BTimeLogger.Wpf.Windows;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Mediator
{
	class PromptToSaveUnsavedChanges : IRequest<bool?>
	{
		public PromptToSaveUnsavedChanges()
		{
		}
	}

	class PromptToSaveUnsavedChangesHandler : IRequestHandler<PromptToSaveUnsavedChanges, bool?>
	{
		private readonly ICsvChangeTracker _csvChangeTracker;
		private readonly IMediator _mediator;
		private readonly IViewManager _viewManager;
		private readonly IUnsavedChangesDialogViewModelFactory _unsavedChangesDialogViewModelFactory;

		public PromptToSaveUnsavedChangesHandler(
			ICsvChangeTracker csvChangeTracker,
			IMediator mediator,
			IViewManager viewManager,
			IUnsavedChangesDialogViewModelFactory unsavedChangesDialogViewModelFactory
			)
		{
			_csvChangeTracker = csvChangeTracker;
			_mediator = mediator;
			_viewManager = viewManager;
			_unsavedChangesDialogViewModelFactory = unsavedChangesDialogViewModelFactory;
		}

		public async Task<bool?> Handle(PromptToSaveUnsavedChanges request, CancellationToken cancellationToken)
		{
			if (!_csvChangeTracker.ChangesMade) return true;

			UnsavedChangesDialogViewModel vm = _unsavedChangesDialogViewModelFactory.Create();

			return _viewManager.ShowDialog(vm);
		}
	}
}
