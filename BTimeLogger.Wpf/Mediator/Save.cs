using BTimeLogger.Csv;
using BTimeLogger.Wpf.Windows;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Mediator
{
	public class Save : IRequest
	{
	}

	public class SaveHandler : IRequestHandler<Save>
	{
		private readonly ICsvLocationPrincipal _csvLocationPrincipal;
		private readonly IIntervalsCsvWriter _intervalsCsvWriter;
		private readonly ISaveAsWindowViewModelFactory _saveAsWindowViewModelFactory;
		private readonly IViewManager _viewManager;
		private readonly ICsvChangeTracker _csvChangeTracker;

		public SaveHandler(
			ICsvLocationPrincipal csvLocationPrincipal,
			IIntervalsCsvWriter intervalsCsvWriter,
			ISaveAsWindowViewModelFactory saveAsWindowViewModelFactory,
			IViewManager viewManager,
			ICsvChangeTracker csvChangeTracker)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_intervalsCsvWriter = intervalsCsvWriter;
			_saveAsWindowViewModelFactory = saveAsWindowViewModelFactory;
			_viewManager = viewManager;
			_csvChangeTracker = csvChangeTracker;
		}

		public async Task<Unit> Handle(Save request, CancellationToken cancellationToken)
		{
			if (!_csvChangeTracker.ChangesMade) return Unit.Value;
			if (_csvLocationPrincipal.LocationsAreSelected)
			{
				await _intervalsCsvWriter.WriteIntervals(_csvLocationPrincipal.IntervalCsvLocation);
				_csvChangeTracker.ClearChanges();
			}
			else
			{
				PromptUserToSaveAs();
			}

			return Unit.Value;
		}

		private void PromptUserToSaveAs()
		{
			SaveAsWindowViewModel vm = _saveAsWindowViewModelFactory.Create();
			_viewManager.ShowDialog(vm);
		}
	}
}
