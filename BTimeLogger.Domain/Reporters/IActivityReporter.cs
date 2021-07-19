using System;

namespace BTimeLogger.Domain
{
	public interface IActivityReporter // TODO: CsvActivityReporter
	{
		ActivityReport Report(DateTime from, DateTime to);
	}
}
