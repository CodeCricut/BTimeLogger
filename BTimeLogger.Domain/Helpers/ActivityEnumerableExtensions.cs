using System.Collections.Generic;
using System.Linq;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Helpers
{
	public static class ActivityEnumerableExtensions
	{
		public static IEnumerable<ActivityCode> SelectCodes(this IEnumerable<Activity> activities)
		{
			return activities.Select(act => act.Code);
		}
	}
}
