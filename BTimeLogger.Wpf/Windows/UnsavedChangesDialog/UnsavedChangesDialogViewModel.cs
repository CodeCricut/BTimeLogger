using MediatR;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Windows
{
	public class UnsavedChangesDialogViewModel : BaseViewModel
	{
		private readonly IMediator _mediator;
		private readonly IViewManager _viewManager;

		private bool? _dialogResult;
		public bool? DialogResult
		{
			get => _dialogResult;
			set { Set(ref _dialogResult, value); }
		}


		public ICommand SaveCommand { get; }
		public ICommand DontSaveCommand { get; }
		public ICommand CancelCommand { get; }

		public UnsavedChangesDialogViewModel(IMediator mediator,
			IViewManager viewManager)
		{
			_mediator = mediator;
			_viewManager = viewManager;

			SaveCommand = new AsyncDelegateCommand(Save);
			DontSaveCommand = new DelegateCommand(DontSave);
			CancelCommand = new DelegateCommand(Cancel);
		}

		private async Task Save(object arg)
		{
			await _mediator.Send(new Mediator.Save());
			ReturnDialogResultAndClose(true);
		}

		private void DontSave(object arg)
		{
			ReturnDialogResultAndClose(true);
		}

		private void Cancel(object arg)
		{
			ReturnDialogResultAndClose(false);
		}

		private void ReturnDialogResultAndClose(bool dialogResult)
		{
			DialogResult = dialogResult;
			_viewManager.Close(this);
		}
	}
}
