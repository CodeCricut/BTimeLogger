using BTimeLogger.Wpf.ViewModels.Domain;
using BTimeLogger.Wpf.ViewModels.Factories;
using System.Windows.Input;
using WpfCore.Commands;
using WpfCore.Services;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class IntervalListItemViewModel : BaseViewModel
	{
		private readonly IModifyIntervalWindowViewModelFactory _modifyIntervalWindowViewModelFactory;
		private readonly IViewManager _viewManager;

		public IntervalViewModel Interval { get; }

		public bool IsLastOnDate { get; }

		public ICommand ModifyIntervalCommand { get; }

		public IntervalListItemViewModel(
			IntervalViewModel intervalVM,
			bool isLastOnDate,
			IModifyIntervalWindowViewModelFactory modifyIntervalWindowViewModelFactory,
			IViewManager viewManager)
		{
			Interval = intervalVM;
			IsLastOnDate = isLastOnDate;
			_modifyIntervalWindowViewModelFactory = modifyIntervalWindowViewModelFactory;
			_viewManager = viewManager;
			ModifyIntervalCommand = new DelegateCommand(ModifyInterval);
		}

		private void ModifyInterval(object obj)
		{
			ModifyIntervalWindowViewModel windowVM = _modifyIntervalWindowViewModelFactory.Create(Interval);
			_viewManager.Show(windowVM);
		}
	}
}
