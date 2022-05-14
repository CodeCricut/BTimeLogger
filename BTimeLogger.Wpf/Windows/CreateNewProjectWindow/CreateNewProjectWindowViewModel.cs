using BTimeLogger.Wpf.Mediator;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BTimeLogger.Wpf.Windows;

public class CreateNewProjectWindowViewModel : BaseViewModel
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
	public ICommand CreateNewProjectCommand { get; }

	public CreateNewProjectWindowViewModel(IMediator mediator, IViewManager viewManager)
	{
		_mediator = mediator;
		_viewManager = viewManager;

		CancelCommand = new DelegateCommand(Cancel);
		CreateNewProjectCommand = new AsyncDelegateCommand(CreateNewProject);
	}

	private async Task CreateNewProject(object arg)
	{
		try
		{
			InvalidFileLocation = false;
			await _mediator.Send(new CreateNewProject(IntervalsCsvLocation));
			_viewManager.Close(this);
		}
		catch (Exception)
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
