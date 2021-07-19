using System;

namespace BTimeLogger.Domain.Reporters
{
	public interface IIntervalsReporter
	{
		IntervalReport Report(ActivityReport activityReport, Activity[] includedActivities);
		IntervalReport Report(ActivityReport activityReport, Activity[] includedActivities, DateTime from, DateTime to);
	}
}
