using Moq;
using NUnit.Framework;

namespace BTimeLogger.Csv.Tests
{
	public class IntervalsCsvReaderTests
	{
		private const string FILE_LOC = @"C:\Users\ajori\OneDrive\Documents\Personal\intervalsCSV.csv";

		[Test]
		public void ReadActivities_InvalidLoc_ThrowsFileNotFound()
		{
			Assert.Fail();
		}


		[Test]
		public void ReadIntervals_InvalidLoc_ThrowsFileNotFound()
		{
			Assert.Fail();
		}


		[Test]
		public void ReadActivities_ValidLoc_ReturnsAllActivities()
		{
			Mock<ICsvPrincipal> csvMock = new Mock<ICsvPrincipal>();
			csvMock.Setup(csvp => csvp.IntervalsCsvLocation).Returns(FILE_LOC);

			IIntervalsCsvReader reader = new IntervalsCsvReader(csvMock.Object);

			var activities = reader.ReadActivities();
		}


		[Test]
		public void ReadIntervals_ValidLoc_ReturnsAllIntervals()
		{
			Assert.Fail();
		}
	}
}
