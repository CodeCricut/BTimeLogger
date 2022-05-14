using System;
using System.Collections.Generic;

namespace BTimeLogger.Wpf.Controls;

public class IncludedIntervalActivitiesChanged
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
