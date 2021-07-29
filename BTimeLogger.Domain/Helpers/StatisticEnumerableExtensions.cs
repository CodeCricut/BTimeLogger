using System;
using System.Collections.Generic;
using System.Linq;

namespace BTimeLogger.Domain.Helpers
{
	public static class StatisticEnumerableExtensions
	{
		public static TimeSpan TotalDuration(this IEnumerable<Statistic> statistics)
		{
			if (statistics.Count() <= 0) return TimeSpan.FromSeconds(0);
			return statistics.Select(stat => stat.Duration).Aggregate((span1, span2) => span1.Add(span2));
		}
	}
}
