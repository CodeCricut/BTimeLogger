namespace BTimeLogger.Wpf.Controls
{
	public interface IActivityViewModelFactory
	{
		ActivityViewModel Create(Activity acvitivy);
	}

	class ActivityViewModelFactory : IActivityViewModelFactory
	{
		public ActivityViewModel Create(Activity activity)
		{
			return new ActivityViewModel(activity);
		}
	}
}
