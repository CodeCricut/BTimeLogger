using System;
using System.Collections.Generic;
using static BTimeLogger.Activity;

namespace BTimeLogger.Wpf.ViewModels.Messages
{
	class IncludedActivitiesChanged
	{
		public IncludedActivitiesChanged(IEnumerable<ActivityCode> newIncludedActivities)
		{
			NewIncludedActivities = newIncludedActivities;
		}

		public static IncludedActivitiesChanged NoIncludedActivities()
		{
			return new IncludedActivitiesChanged(Array.Empty<ActivityCode>());
		}

		public static IncludedActivitiesChanged SingleActivity(ActivityCode activityCode)
		{
			return new IncludedActivitiesChanged(new ActivityCode[] { activityCode });
		}

		public IEnumerable<ActivityCode> NewIncludedActivities { get; }
	}
}
