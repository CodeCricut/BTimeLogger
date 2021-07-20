using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.Domain
{
	public class ActivityViewModel : BaseViewModel
	{
		private readonly Activity _activity;

		public string Name { get => _activity.Name; }
		public bool IsGroup { get => _activity.IsGroup; }
		//public ActivityViewModel Parent { get; set; }
		public bool HasParent { get => _activity.HasParent; }
		//public ActivityViewModel[] Children { get; set; }

		public ActivityViewModel(Activity activity)
		{
			_activity = activity;
		}
	}
}
