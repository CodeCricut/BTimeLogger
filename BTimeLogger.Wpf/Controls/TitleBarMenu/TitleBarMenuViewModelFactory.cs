using BTimeLogger.Wpf.Services;
using BTimeLogger.Wpf.Windows;
using MediatR;

namespace BTimeLogger.Wpf.Controls;

public interface ITitleBarMenuViewModelFactory
{
	TitleBarMenuViewModel Create();
}

class TitleBarMenuViewModelFactory : ITitleBarMenuViewModelFactory
{
	private readonly IViewManager _viewManager;
	private readonly ICreateNewProjectWindowViewModelFactory _createNewProjectWindowViewModelFactory;
	private readonly IOpenCsvsWindowViewModelFactory _openCsvsWindowViewModelFactory;
	private readonly IOpenRecentCsvsWindowViewModelFactory _openRecentCsvsWindowViewModelFactory;
	private readonly IMediator _mediator;
	private readonly ISaveAsWindowViewModelFactory _saveAsWindowViewModelFactory;
	private readonly ISkinManager _skinManager;

	public TitleBarMenuViewModelFactory(IViewManager viewManager,
		ICreateNewProjectWindowViewModelFactory createNewProjectWindowViewModelFactory,
		IOpenCsvsWindowViewModelFactory openCsvsWindowViewModelFactory,
		IOpenRecentCsvsWindowViewModelFactory openRecentCsvsWindowViewModelFactory,
		IMediator mediator,
		ISaveAsWindowViewModelFactory saveAsWindowViewModelFactory,
		ISkinManager skinManager)
	{
		_viewManager = viewManager;
		_createNewProjectWindowViewModelFactory = createNewProjectWindowViewModelFactory;
		_openCsvsWindowViewModelFactory = openCsvsWindowViewModelFactory;
		_openRecentCsvsWindowViewModelFactory = openRecentCsvsWindowViewModelFactory;
		_mediator = mediator;
		_saveAsWindowViewModelFactory = saveAsWindowViewModelFactory;
		_skinManager = skinManager;
	}

	public TitleBarMenuViewModel Create()
	{
		return new TitleBarMenuViewModel(_viewManager,
								_createNewProjectWindowViewModelFactory,
								_openCsvsWindowViewModelFactory,
								_openRecentCsvsWindowViewModelFactory,
								_saveAsWindowViewModelFactory,
								_mediator,
								_skinManager);
	}
}
