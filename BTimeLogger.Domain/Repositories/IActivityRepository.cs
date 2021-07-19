using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Domain
{
	public interface IActivityRepository
	{
		Task<IQueryable<Activity>> GetActivities();
		Task<Activity> GetActivity(int id);
		Task<bool> ActivityExists(int id);
	}
}
