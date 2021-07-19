using System;

namespace BTimeLogger.Domain.Reporters
{
	public interface IIntervalsReporter
	{
		IntervalReport Report(ActivityReport activityReport, Activity[] includedActivities);
		IntervalReport Report(ActivityReport activityReport, Activity[] includedActivities, DateTime from, DateTime to);
	}

	public class IntervalsReporter : IIntervalsReporter
	{
		public IntervalReport Report(ActivityReport activityReport, Activity[] includedActivities)
		{
			return new IntervalReport(); // TODO
		}

		public IntervalReport Report(ActivityReport activityReport, Activity[] includedActivities, DateTime from, DateTime to)
		{
			return new IntervalReport(); // TODO

		}
	}
}
