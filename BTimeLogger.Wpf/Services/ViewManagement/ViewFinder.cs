using HackerNews.WPF.Core.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Services.ViewManagement
{
	interface IViewFinder
	{
		Window FindViewForViewModel<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;
	}

	class ViewFinder : IViewFinder
	{
		private readonly IServiceProvider _viewServiceProvider;

		public ViewFinder(IServiceProvider viewServiceProvider)
		{
			_viewServiceProvider = viewServiceProvider;
		}

		public Window FindViewForViewModel<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
		{
			Type viewType = GetAssociatedViewType(viewModel.GetType());
			if (viewType == null) throw new ViewForViewModelNotFoundException($"Could not find a view associated with {viewModel}.");

			bool viewIsWindow = viewType.IsSubclassOf(typeof(Window)) || viewType == typeof(Window);
			if (!viewIsWindow) throw new AssociatedViewNotWindowException(
			   $"The view associated with {viewModel} was not a Window.");

			return (Window)_viewServiceProvider.GetRequiredService(viewType);
		}

		private Type GetAssociatedViewType(Type viewModelType)
		{
			System.Collections.Generic.IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes());

			Type viewForVM = types
				.FirstOrDefault(viewType => viewType
					.GetInterfaces()
						.Any(i =>
							i.IsGenericType &&
							i.GetGenericTypeDefinition() == typeof(IHaveViewModel<>) &&
							i.GenericTypeArguments.Contains(viewModelType)));

			return viewForVM;
		}
	}
}
