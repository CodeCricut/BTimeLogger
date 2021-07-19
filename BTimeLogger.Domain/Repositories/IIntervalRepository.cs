using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger
{
	public interface IIntervalRepository
	{
		Task<IQueryable<Interval>> GetIntervals();
	}
}
