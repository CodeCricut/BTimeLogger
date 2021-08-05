using BTimeLogger.Csv.Helpers;
using BTimeLogger.Domain.Services;
using CsvHelper;
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

		private IOrderedEnumerable<Interval> _intervals;
		private int _numGroupCols = 0;

		public IntervalsCsvWriter(IIntervalRepository intervalRepository)
		{
			_intervalRepository = intervalRepository;
		}

		public async Task WriteIntervals(string fileLocation)
		{
			using StreamWriter writer = new(fileLocation, false);
			using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);

			_intervals = (await _intervalRepository.GetIntervals())
				.OrderByDescending(interval => interval.From);
			_numGroupCols = GetNumGroupColumns();

			WriteHeader(csv);
			await WriteRecords(csv);
		}

		private void WriteHeader(CsvWriter csv)
		{
			for (int i = 0; i < _numGroupCols; i++)
				csv.WriteField("Group");

			csv.WriteField("Activity type");
			csv.WriteField("Duration");
			csv.WriteField("From");
			csv.WriteField("To");
			csv.WriteField("Comment");

			csv.NextRecord();
		}

		private Task WriteRecords(CsvWriter csv)
		{
			return Task.Factory.StartNew(() =>
			{
				foreach (var interval in _intervals)
					WriteIntervalAsRecord(csv, interval);
			});
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

			int numEmptyCols = _numGroupCols - groupNames.Length;

			WriteEmptyGroupCols(csv, numEmptyCols);

			for (int i = 0; i < groupNames.Length; i++)
			{
				csv.WriteField(groupNames[i]);
			}
		}

		private void WriteEmptyGroupCols(CsvWriter csv, int numEmptyCols)
		{
			for (int i = 0; i < numEmptyCols; i++)
			{
				csv.WriteField(string.Empty);
			}
		}

		private int GetNumGroupColumns()
		{
			if (_intervals.Count() <= 0) return 0;

			Interval intervalWithMostAncestors = _intervals.OrderByDescending(interval =>
				interval.Activity.Code.Parts.Length).First();

			int numGroupColumns = intervalWithMostAncestors.Activity.Code.AncestorCodes.Count() - 1;
			return numGroupColumns;
		}
	}
}
