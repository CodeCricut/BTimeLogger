using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger
{
	public interface IIntervalRepository
	{
		Task<IQueryable<Interval>> GetIntervals();
		Task<Interval> GetInterval(int id);
		Task<bool> IntervalExists(int id);
	}
}
