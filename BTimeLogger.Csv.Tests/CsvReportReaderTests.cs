using Moq;
using NUnit.Framework;

namespace BTimeLogger.Csv.Tests
{
	public class CsvReportReaderTests
	{
		private const string FILE_LOC = @"C:\Users\ajori\OneDrive\Documents\Personal\PRE-July14_2021.csv";

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
		public void ReadStatistics_InvalidLoc_ThrowsFileNotFound()
		{
			Assert.Fail();
		}

		[Test]
		public void ReadActivities_ValidLoc_ReturnsAllActivities()
		{
			Mock<ICsvPrincipal> csvMock = new Mock<ICsvPrincipal>();
			csvMock.Setup(csvp => csvp.CsvFileLocation).Returns(FILE_LOC);

			ICsvReportReader reader = new CsvReportReader(csvMock.Object);

			var activities = reader.ReadActivities();
		}


		[Test]
		public void ReadIntervals_ValidLoc_ReturnsAllIntervals()
		{
			Assert.Fail();
		}

		[Test]
		public void ReadStatistics_ValidLoc_ReturnsAllStatistics()
		{
			Assert.Fail();
		}
	}
}
