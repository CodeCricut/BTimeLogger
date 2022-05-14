using System;
using System.Collections.Generic;

namespace BTimeLogger.Wpf.Model;

public class IntervalSearchFilter
{
	public DateTime From { get; set; }
	public DateTime To { get; set; }
	public IEnumerable<ActivityCode> IncludedActivities { get; set; } = Array.Empty<ActivityCode>();
}
