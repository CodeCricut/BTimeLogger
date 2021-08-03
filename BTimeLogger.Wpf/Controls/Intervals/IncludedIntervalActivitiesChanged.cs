using System;
using System.Collections.Generic;
using static BTimeLogger.Activity;

namespace BTimeLogger.Wpf.Controls
{
	class IncludedIntervalActivitiesChanged
	{
		public IncludedIntervalActivitiesChanged(IEnumerable<ActivityCode> newIncludedActivities)
		{
			NewIncludedActivities = newIncludedActivities;
		}

		public static IncludedIntervalActivitiesChanged NoIncludedActivities()
		{
			return new IncludedIntervalActivitiesChanged(Array.Empty<ActivityCode>());
		}

		public static IncludedIntervalActivitiesChanged SingleActivity(ActivityCode activityCode)
		{
			return new IncludedIntervalActivitiesChanged(new ActivityCode[] { activityCode });
		}

		public IEnumerable<ActivityCode> NewIncludedActivities { get; }
	}
}
