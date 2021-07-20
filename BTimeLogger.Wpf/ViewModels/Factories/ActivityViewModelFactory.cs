using BTimeLogger.Wpf.ViewModels.Domain;

namespace BTimeLogger.Wpf.ViewModels.Factories
{
	public interface IActivityViewModelFactory
	{
		ActivityViewModel Create(Activity acvitivy);
	}

	class ActivityViewModelFactory : IActivityViewModelFactory
	{
		public ActivityViewModel Create(Activity activity)
		{
			return new(activity);
		}
	}
}
