using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BTimeLogger.Domain
{
	public class ActivityNameEqualityOperator : IEqualityComparer<Activity>
	{
		public bool Equals(Activity x, Activity y)
		{
			if (x == null || x.Name == null || y == null) return false;
			return x.Name.Equals(y.Name);
		}

		public int GetHashCode([DisallowNull] Activity obj)
		{
			if (obj == null) return 0;
			return obj.Name.GetHashCode();
		}
	}
}
