﻿namespace BTimeLogger.Domain.Reporters
{
	public interface IStatisticsReporter
	{
		StatisticsReport Report(ActivityReport activityReport, Activity[] includedActivities);
	}
}
