using CsvHelper.Configuration.Attributes;

namespace BTimeLogger.Csv;

/// <summary>
/// This class represents a record in a CSV file which stores information
/// about an <see cref="Interval"/>.
/// </summary>
public class CsvIntervalRecord
{
	public string[] Groups { get; set; }
	public int NumGroups { get => Groups.Length; }

	[Name("Activity type")]
	public string ActivityType { get; set; }
	[Name("Duration")]
	public string Duration { get; set; }
	[Name("From")]
	public string From { get; set; }
	[Name("To")]
	public string To { get; set; }
	[Name("Comment")]
	public string Comment { get; set; }
}
