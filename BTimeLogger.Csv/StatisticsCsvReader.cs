using BTimeLogger.Domain;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Csv
{
	public interface IStatisticsCsvReader
	{
		Task ReadStatisticsCsv(string statisticsCsvLocation);
	}

	class StatisticsCsvReader : ReportCsvReader, IStatisticsCsvReader
	{
		protected const int BASE_STAT_COL_COUNT = 3;

		private readonly IActivityRepository _activityRepository;
		private readonly IStatisticsRepository _statisticsRepository;

		public StatisticsCsvReader(IIntervalRepository intervalRepository, IActivityRepository activityRepository, IStatisticsRepository statisticsRepository) : base(intervalRepository, activityRepository, statisticsRepository)
		{
			_activityRepository = activityRepository;
			_statisticsRepository = statisticsRepository;
		}

		public Task ReadStatisticsCsv(string csvLocation)
		{
			return ReadCsv(csvLocation);
		}

		public override async Task ReadRowData(CsvReader csv)
		{
			CsvStatisticRecord record = GetRowRecord(csv);
			ActivityCode code = ActivityCode.CreateCode(record.ActivityType, record.Groups);

			await AddNewGroupActivities(code.ParentCode);
			await AddActivityIfNew(code, false);
			await AddStatistic(record);
		}

		private async Task AddStatistic(CsvStatisticRecord record)
		{
			ActivityCode code = ActivityCode.CreateCode(record.ActivityType, record.Groups);
			Activity activity = await _activityRepository.GetActivity(code);
			if (activity == null) throw new KeyNotFoundException("Cannot create statistic when activity type is not read.");

			TimeSpan duration = CsvFieldParser.ParseDuration(record.Duration);
			decimal percent = CsvFieldParser.ParsePercent(record.Percent);

			Statistic statistic = new()
			{
				Activity = activity,
				Duration = duration,
				Percent = percent
			};

			await _statisticsRepository.AddStatistic(statistic);
		}

		private CsvStatisticRecord GetRowRecord(CsvReader csv)
		{
			CsvStatisticRecord record = csv.GetRecord<CsvStatisticRecord>();
			record.Groups = ReadRecordGroupNames(csv);
			return record;
		}

		protected override int GetGroupColCount(CsvReader csv)
		{
			int count = csv.GetFieldIndex("%") + 1 - BASE_STAT_COL_COUNT;
			return count;
		}
	}
}
