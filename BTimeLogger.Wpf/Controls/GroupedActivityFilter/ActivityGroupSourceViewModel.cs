using System.Collections.ObjectModel;
using System.Linq;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class ActivityGroupSourceViewModel : BaseViewModel
	{
		public ObservableCollection<ActivityViewModel> Items { get; } = new();

		private ActivityViewModel _selectedGroupActivity;
		public ActivityViewModel SelectedGroupActivity
		{
			get => _selectedGroupActivity;
			set
			{
				Set(ref _selectedGroupActivity, value);
				RaisePropertyChanged(ALL_PROPS_CHANGED);
			}
		}

		public void SelectActivity(ActivityViewModel activity)
		{
			bool HasEqualActivityCodes(ActivityViewModel avm) => avm.Activity.Code.Equals(activity.Activity.Code);
			if (!Items.Any(HasEqualActivityCodes))
				Items.Add(activity);

			SelectedGroupActivity = Items.First(HasEqualActivityCodes);
		}

		public bool NoneItemSelected { get => SelectedGroupActivity is NoneItem; }
		public bool NoActivityGroupSelected { get => NoneItemSelected || SelectedGroupActivity == null; }
	}
}
