using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			if (_activities.ContainsKey(activity.Code))
				throw new System.Exception();

			if (activity.HasParent)
			{
				bool parentExists = _unsavedActivities.TryGetValue(activity.Code.ParentCode, out Activity parent);
				if (!parentExists) throw new KeyNotFoundException();

				if (!parent.Children.Contains(activity))
					parent.Children.Add(activity);
			}

			_unsavedActivities.Add(activity.Code, activity);

			return Task.CompletedTask;
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

		public Task RemoveChanges()
		{
			return Task.Factory.StartNew(() =>
			{
				_unsavedActivities.Clear();
				foreach (var savedActivity in _activities)
				{
					_unsavedActivities.Add(savedActivity.Key, savedActivity.Value);
				}
			});
		}
	}
}
