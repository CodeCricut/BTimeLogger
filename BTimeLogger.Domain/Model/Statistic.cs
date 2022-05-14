using System;

namespace BTimeLogger;

/// <summary>
/// Statistical data about time spent doing a certain activity within a specific timespan.
/// </summary>
public class Statistic
{
	public Activity Activity { get; set; }
	// TODO: add documentation to specify whether this is duration of the statistic, or total duration of the activity within the timespan
	public TimeSpan Duration { get; set; }
	public decimal PercentOfTrackedTimeInTimespan { get; set; }

	public DateTime From { get; set; }
	public DateTime To { get; set; }
}
