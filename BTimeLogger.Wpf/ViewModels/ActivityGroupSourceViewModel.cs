using BTimeLogger.Wpf.ViewModels.Domain;
using System;
using System.Collections.ObjectModel;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels
{
	public class ActivityGroupSourceViewModel : BaseViewModel
	{
		public class NoneItem : ActivityViewModel
		{
			public NoneItem() : base(new Activity()
			{
				Children = Array.Empty<Activity>(),
				IsGroup = true,
				Name = "None",
				Parent = null
			})
			{
			}
		}

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
			if (!Items.Contains(activity))
				Items.Add(activity);

			SelectedGroupActivity = activity;
		}

		public bool NoneItemSelected { get => SelectedGroupActivity is NoneItem; }
		public bool NoActivityGroupSelected { get => NoneItemSelected || SelectedGroupActivity == null; }
	}
}
