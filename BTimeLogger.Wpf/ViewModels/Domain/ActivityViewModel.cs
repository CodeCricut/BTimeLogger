using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.Domain
{
	public class ActivityViewModel : BaseViewModel
	{
		public Activity Activity { get; }

		public string Name { get => Activity.Name; }
		public string CodeValue { get => Activity.Code.Value; }
		public bool IsGroup { get => Activity.IsGroup; }
		public bool HasParent { get => Activity.HasParent; }


		public ActivityViewModel(Activity activity)
		{
			Activity = activity;
		}
	}
}
