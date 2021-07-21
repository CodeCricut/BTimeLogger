using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger
{
	public interface IIntervalRepository
	{
		Task<IQueryable<Interval>> GetIntervals();
		Task<IQueryable<Interval>> GetIntervals(Activity[] activities, DateTime from, DateTime to);
	}
}
