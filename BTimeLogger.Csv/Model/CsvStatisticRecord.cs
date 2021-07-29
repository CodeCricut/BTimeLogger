using CsvHelper.Configuration.Attributes;

namespace BTimeLogger.Csv
{
	public class CsvStatisticRecord
	{
		public string[] Groups { get; set; }
		public int NumGroups { get => Groups.Length; }

		[Name("Activity type")]
		public string ActivityType { get; set; }
		[Name("Duration")]
		public string Duration { get; set; }
		[Name("%")]
		public string Percent { get; set; }
	}
}
