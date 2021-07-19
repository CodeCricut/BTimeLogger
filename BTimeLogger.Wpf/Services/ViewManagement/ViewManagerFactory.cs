using System;
using WpfCore.Services;

namespace BTimeLogger.Wpf.Services.ViewManagement
{
	interface IViewManagerFactory
	{
		IViewManager CreateWithAppServices();
		IViewManager CreateWithServices(IServiceProvider serviceProvider);
	}

	class ViewManagerFactory : IViewManagerFactory
	{
		private readonly IViewFinderFactory _viewFinderFactory;

		public ViewManagerFactory(IViewFinderFactory viewFinderFactory)
		{
			_viewFinderFactory = viewFinderFactory;
		}

		public IViewManager CreateWithAppServices()
		{
			var viewFinder = _viewFinderFactory.CreateWithAppServices();
			return new ViewManager(viewFinder);
		}

		public IViewManager CreateWithServices(IServiceProvider serviceProvider)
		{
			var viewFinder = _viewFinderFactory.Create(serviceProvider);
			return new ViewManager(viewFinder);
		}
	}
}
