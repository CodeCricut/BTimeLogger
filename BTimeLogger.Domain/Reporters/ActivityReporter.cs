using System;

namespace BTimeLogger.Domain
{
	public interface IActivityReporter
	{
		ActivityReport Report(DateTime from, DateTime to);
	}

	public class ActivityReporter : IActivityReporter
	{
		public ActivityReport Report(DateTime from, DateTime to)
		{
			return new ActivityReport(); // TODO
		}
	}
}
