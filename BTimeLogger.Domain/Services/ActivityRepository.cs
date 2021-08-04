using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Services
{
	public interface IActivityRepository : IRepository
	{
		Task<IEnumerable<Activity>> GetActivities();
		Task<Activity> GetActivity(ActivityCode code);
		Task<bool> ActivityExists(ActivityCode code);
		Task AddActivity(Activity group);
	}

	class ActivityRepository : IActivityRepository
	{
		private readonly Dictionary<ActivityCode, Activity> _activities = new();
		private readonly Dictionary<ActivityCode, Activity> _unsavedActivities = new();

		public Task<bool> ActivityExists(ActivityCode code)
		{
			if (code == null)
				return Task.FromResult(false);
			return Task.FromResult(_unsavedActivities.ContainsKey(code));
		}

		public Task AddActivity(Activity activity)
		{
			return Task.Factory.StartNew(() => _unsavedActivities.Add(activity.Code, activity));
		}

		public Task<IEnumerable<Activity>> GetActivities()
		{
			return Task.FromResult(_activities.Select(kvp => kvp.Value));
		}

		public Task<Activity> GetActivity(ActivityCode code)
		{
			if (code == null) return Task.FromResult<Activity>(null);
			return Task.FromResult(_activities.GetValueOrDefault(code));
		}

		public Task Clear()
		{
			_unsavedActivities.Clear();
			return Task.CompletedTask;
		}

		public Task SaveChanges()
		{
			return Task.Factory.StartNew(() =>
			{
				_activities.Clear();
				foreach (var unsavedActivity in _unsavedActivities)
				{
					_activities.Add(unsavedActivity.Key, unsavedActivity.Value);
				}
			});
		}
	}
}
