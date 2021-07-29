using BTimeLogger.Domain;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Csv
{
	public interface IIntervalsCsvReader
	{
		Task ReadIntervalCsv(string fileLocation);
	}

	class IntervalsCsvReader : ReportCsvReader, IIntervalsCsvReader
	{
		protected const int BASE_INTERVAL_COLUMN_COUNT = 5;

		private readonly IIntervalRepository _intervalRepository;
		private readonly IActivityRepository _activityRepository;

		public IntervalsCsvReader(
			IIntervalRepository intervalRepository,
			IActivityRepository activityRepository,
			IStatisticsRepository statisticsRepository) : base(intervalRepository, activityRepository, statisticsRepository)
		{
			_intervalRepository = intervalRepository;
			_activityRepository = activityRepository;
		}

		public Task ReadIntervalCsv(string fileLocation)
		{
			return ReadCsv(fileLocation);
		}

		public override async Task ReadRowData(CsvReader csv)
		{
			CsvIntervalRecord record = GetRowRecord(csv);
			ActivityCode activityCode = ActivityCode.CreateCode(record.ActivityType, record.Groups);

			await AddNewGroupActivities(activityCode.ParentCode);
			await AddActivityIfNew(activityCode, false);
			await AddInterval(record);
		}

		private CsvIntervalRecord GetRowRecord(CsvReader csv)
		{
			CsvIntervalRecord record = csv.GetRecord<CsvIntervalRecord>();
			record.Groups = ReadRecordGroupNames(csv);
			return record;
		}

		private async Task AddInterval(CsvIntervalRecord record)
		{
			ActivityCode code = ActivityCode.CreateCode(record.ActivityType, record.Groups);
			Activity activity = await _activityRepository.GetActivity(code);
			if (activity == null)
				throw new KeyNotFoundException("Cannot create interval when activity type is not read.");

			DateTime fromDate = CsvFieldParser.ParseDate(record.From);
			DateTime to = CsvFieldParser.ParseDate(record.To);
			TimeSpan duration = to - fromDate;

			Interval interval = new()
			{
				Activity = activity,
				Comment = record.Comment,
				Duration = duration,
				From = fromDate,
				To = to
			};

			await _intervalRepository.AddInterval(interval);
		}

		protected override int GetGroupColCount(CsvReader csv)
		{
			return csv.GetFieldIndex("Comment") + 1 - BASE_INTERVAL_COLUMN_COUNT;
		}
	}
}
