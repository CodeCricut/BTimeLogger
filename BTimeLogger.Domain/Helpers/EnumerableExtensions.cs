using System.Collections.Generic;
using System.Linq;

namespace BTimeLogger.Domain.Helpers;

public static class EnumerableExtensions
{
	public static IEnumerable<T> Union<T>(this IEnumerable<T> source, params T[] items)
	{
		return source.Union((IEnumerable<T>)items);
	}
}
