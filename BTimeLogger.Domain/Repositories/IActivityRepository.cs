using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Domain
{
	public interface IActivityRepository
	{
		Task<IQueryable<Activity>> GetActivities();
		Task<Activity> GetActivity(string name);
		Task<bool> ActivityExists(string name);
	}
}
