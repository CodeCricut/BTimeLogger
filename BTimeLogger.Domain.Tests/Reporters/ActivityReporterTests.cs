using NUnit.Framework;

namespace BTimeLogger.Domain.Tests.Reporters
{
	public class ActivityReporterTests
	{
		[Test]
		public void Report_NullFrom_ThrowsArgumentNull()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_NullTo_ThrowsArgumentNull()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_FromAfterTo_ThrowsInvalidArgument()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_ValidToFrom_CreatesWithIntervalsBetween()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_ValidToFrom_CreatesWithStatisticsBetween()
		{
			Assert.Fail();
		}

		[Test]
		public void Report_ValidToFrom_CreatesWithToFrom()
		{
			Assert.Fail();
		}
	}
}
