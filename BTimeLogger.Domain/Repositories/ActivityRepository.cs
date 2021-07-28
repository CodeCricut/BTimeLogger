using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Domain
{
	public interface IActivityRepository
	{
		Task<IEnumerable<Activity>> GetActivities();
		Task<Activity> GetActivity(string name);
		Task<bool> ActivityExists(string name);
		Task AddActivity(Activity group);
	}

	class ActivityRepository : IActivityRepository
	{
		private readonly Dictionary<string, Activity> _activities = new();

		public Task<bool> ActivityExists(string name)
		{
			return Task.FromResult(_activities.ContainsKey(name));
		}

		public Task AddActivity(Activity group)
		{
			return Task.Factory.StartNew(() => _activities.Add(group.Name, group));
		}

		public Task<IEnumerable<Activity>> GetActivities()
		{
			return Task.FromResult(_activities.Select(kvp => kvp.Value));
		}

		public Task<Activity> GetActivity(string name)
		{
			return Task.FromResult(_activities.GetValueOrDefault(name));
		}
	}
}
