using BTimeLogger.Csv.Helpers;
using BTimeLogger.Domain.Services;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	public interface IIntervalsCsvWriter
	{
		Task WriteIntervals(string fileLocation);

	}

	class IntervalsCsvWriter : IIntervalsCsvWriter
	{
		private readonly IIntervalRepository _intervalRepository;

		public IntervalsCsvWriter(IIntervalRepository intervalRepository)
		{
			_intervalRepository = intervalRepository;
		}

		public async Task WriteIntervals(string fileLocation)
		{
			using StreamWriter writer = new(fileLocation, false);
			using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);

			WriteHeader(csv);
			await WriteRecords(csv);
		}

		private void WriteHeader(CsvWriter csv)
		{
			for (int i = 0; i < GetNumGroupColumns(); i++)
				csv.WriteField("Group");

			csv.WriteField("Activity type");
			csv.WriteField("Duration");
			csv.WriteField("From");
			csv.WriteField("To");
			csv.WriteField("Comment");

			csv.NextRecord();
		}

		private async Task WriteRecords(CsvWriter csv)
		{
			IEnumerable<Interval> intervals = (await _intervalRepository.GetIntervals())
				.OrderByDescending(interval => interval.From);
			foreach (var interval in intervals)
				WriteIntervalAsRecord(csv, interval);
		}

		private void WriteIntervalAsRecord(CsvWriter csv, Interval interval)
		{
			WriteGroupCols(csv, interval);

			csv.WriteField(interval.Activity.Name);
			csv.WriteField(interval.Duration.ToCsvFormat());
			csv.WriteField(interval.From.ToCsvFormat());
			csv.WriteField(interval.To.ToCsvFormat());
			csv.WriteField(interval.Comment, shouldQuote: true);

			csv.NextRecord();
		}

		private void WriteGroupCols(CsvWriter csv, Interval interval)
		{
			string[] groupNames = interval.Activity.Code.GroupNames;

			WriteEmptyGroupCols(csv, groupNames.Length);

			for (int i = 0; i < groupNames.Length; i++)
			{
				csv.WriteField(groupNames[i]);
			}
		}

		private void WriteEmptyGroupCols(CsvWriter csv, int numGroups)
		{
			int totalGroupCols = GetNumGroupColumns();
			int numEmptyCols = totalGroupCols - numGroups;
			for (int i = 0; i < numEmptyCols; i++)
			{
				csv.WriteField(string.Empty);
			}
		}

		private int GetNumGroupColumns()
		{
			// TODO: should be based on depth of activity groups. Not sure how to do that easily...
			return 3;
		}
	}
}
