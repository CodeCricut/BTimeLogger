using NUnit.Framework;

namespace BTimeLogger.Domain.Tests.Reporters
{
	public class StatisticsReporterTests
	{
		[Test]
		public void Report_NullActivityReport_ThrowsArgumentNull()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_NoActivities_ReturnsNoStatistics()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_AllActivities_ReturnsAllStatistics()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_SomeActivities_ReturnsMatchingStatistics()
		{
			Assert.Fail();
		}
	}
}
