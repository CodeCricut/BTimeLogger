using HackerNews.WPF.Core.View;
using System.Collections.Generic;
using System.Windows;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Services.ViewManagement
{
	class ViewManager : IViewManager
	{
		private readonly Dictionary<BaseViewModel, Window> _activeViews = new Dictionary<BaseViewModel, Window>();
		private readonly IViewFinder _viewFinder;

		public ViewManager(IViewFinder viewFinder)
		{
			_viewFinder = viewFinder;
		}

		public void Show<TViewModel>(TViewModel viewModel)
			where TViewModel : BaseViewModel
		{
			if (_activeViews.ContainsKey(viewModel))
				throw new ViewAlreadyShowingException($"View for {viewModel} view model already showing. Ensure that the view is closed before opening it again.");

			Window view = _viewFinder.FindViewForViewModel(viewModel);

			Application.Current.Dispatcher.Invoke(() =>
			{
				((IHaveViewModel<TViewModel>)view).SetViewModel(viewModel);
				view.Show();

				_activeViews.Add(viewModel, view);
			});
		}

		public void Close(BaseViewModel viewModel)
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				if (!_activeViews.ContainsKey(viewModel)) return false;

				var view = _activeViews.GetValueOrDefault(viewModel);

				view?.Close();

				return _activeViews.Remove(viewModel);
			});
		}
	}
}
