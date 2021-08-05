using BTimeLogger.Wpf.Mediator;
using MediatR;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Windows
{
	public class SaveAsWindowViewModel : BaseViewModel
	{
		private readonly IMediator _mediator;
		private readonly IViewManager _viewManager;
		private string _intervalsCsvLocation;

		public string IntervalsCsvLocation
		{
			get { return _intervalsCsvLocation; }
			set { Set(ref _intervalsCsvLocation, value); }
		}

		private bool _invalidFileLocation;
		public bool InvalidFileLocation
		{
			get { return _invalidFileLocation; }
			set { Set(ref _invalidFileLocation, value); }
		}

		public ICommand CancelCommand { get; }
		public ICommand SaveAsCommand { get; }

		public SaveAsWindowViewModel(IMediator mediator, IViewManager viewManager)
		{
			_mediator = mediator;
			_viewManager = viewManager;

			CancelCommand = new DelegateCommand(Cancel);
			SaveAsCommand = new AsyncDelegateCommand(SaveAs);
		}

		private async Task SaveAs(object arg)
		{
			try
			{
				InvalidFileLocation = false;
				await _mediator.Send(new SaveAs(IntervalsCsvLocation));
				_viewManager.Close(this);
			}
			catch (System.Exception)
			{
				IntervalsCsvLocation = string.Empty;
				InvalidFileLocation = true;
			}
		}

		private void Cancel(object obj)
		{
			_viewManager.Close(this);
		}
	}
}
