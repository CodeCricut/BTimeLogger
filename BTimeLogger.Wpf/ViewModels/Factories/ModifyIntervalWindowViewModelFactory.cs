using BTimeLogger.Wpf.ViewModels.Domain;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IModifyIntervalWindowViewModelFactory
	{
		ModifyIntervalWindowViewModel Create(IntervalViewModel intervalViewModel);
		ModifyIntervalWindowViewModel Create(ModifyIntervalViewModel modifyIntervalViewModel);
	}
	class ModifyIntervalWindowViewModelFactory : IModifyIntervalWindowViewModelFactory
	{
		private readonly IModifyIntervalViewModelFactory _modifyIntervalViewModelFactory;
		private readonly IWindowButtonsViewModelFactory _windowButtonsViewModelFactory;
		private readonly IGroupFilterViewModelFactory _groupFilterViewModelFactory;
		private readonly ITimeSpanPanelViewModelFactory _timeSpanPanelViewModelFactory;
		private readonly IActivityTypeSelectorViewModelFactory _activityTypeSelectorViewModelFactory;

		public ModifyIntervalWindowViewModelFactory(IModifyIntervalViewModelFactory modifyIntervalViewModelFactory,
			IWindowButtonsViewModelFactory windowButtonsViewModelFactory,
			IGroupFilterViewModelFactory groupFilterViewModelFactory,
			ITimeSpanPanelViewModelFactory timeSpanPanelViewModelFactory,
			IActivityTypeSelectorViewModelFactory activityTypeSelectorViewModelFactory)
		{
			_modifyIntervalViewModelFactory = modifyIntervalViewModelFactory;
			_windowButtonsViewModelFactory = windowButtonsViewModelFactory;
			_groupFilterViewModelFactory = groupFilterViewModelFactory;
			_timeSpanPanelViewModelFactory = timeSpanPanelViewModelFactory;
			_activityTypeSelectorViewModelFactory = activityTypeSelectorViewModelFactory;
		}

		public ModifyIntervalWindowViewModel Create(IntervalViewModel intervalViewModel)
		{
			// TODO: messy but yolo
			var activityTypeSelectorVM = _activityTypeSelectorViewModelFactory.Create();
			ModifyIntervalViewModel modifyIntervalVM = _modifyIntervalViewModelFactory.Create(intervalViewModel, activityTypeSelectorVM);

			return Create(modifyIntervalVM);
		}

		public ModifyIntervalWindowViewModel Create(ModifyIntervalViewModel modifyIntervalViewModel)
		{
			var windowButtonsVM = _windowButtonsViewModelFactory.Create();
			return new ModifyIntervalWindowViewModel(modifyIntervalViewModel, windowButtonsVM);
		}
	}
}
