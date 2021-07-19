using System;
using System.Windows;

namespace BTimeLogger.Wpf.Services.ViewManagement
{
	interface IViewFinderFactory
	{
		IViewFinder CreateWithAppServices();
		IViewFinder Create(IServiceProvider serviceProvider);
	}

	class ViewFinderFactory : IViewFinderFactory
	{
		public IViewFinder CreateWithAppServices()
		{
			var sp = (Application.Current as App).Services;
			return Create(sp);
		}

		public IViewFinder Create(IServiceProvider serviceProvider)
		{
			return new ViewFinder(serviceProvider);
		}
	}
}
