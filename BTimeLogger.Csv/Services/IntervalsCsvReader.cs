using BTimeLogger.Csv.Helpers;
using BTimeLogger.Domain.Services;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace BTimeLogger.Csv.Services
{
	public interface IIntervalsCsvReader
	{
		Task ReadIntervalCsv(string fileLocation);
	}

	class IntervalsCsvReader : IIntervalsCsvReader
	{
		protected const int BASE_INTERVAL_COLUMN_COUNT = 5;
		private int _groupColCount;

		private readonly IIntervalRepository _intervalRepository;
		private readonly IActivityRepository _activityRepository;

		public IntervalsCsvReader(
			IIntervalRepository intervalRepository,
			IActivityRepository activityRepository)
		{
			_intervalRepository = intervalRepository;
			_activityRepository = activityRepository;
		}

		public async Task ReadIntervalCsv(string fileLocation)
		{
			using StreamReader reader = new(fileLocation);
			using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

			csv.Read();
			csv.ReadHeader();
			while (csv.Read())
			{
				InitializeNumColums(csv);

				await ReadRowData(csv);
			}

			await _intervalRepository.SaveChanges();
			await _activityRepository.SaveChanges();
		}

		private void InitializeNumColums(CsvReader csv)
		{
			_groupColCount = GetGroupColCount(csv);
		}

		public async Task ReadRowData(CsvReader csv)
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

		private async Task AddNewGroupActivities(ActivityCode groupCode)
		{
			List<ActivityCode> ancestorCodes = new();

			ActivityCode currentCode = groupCode;
			while (currentCode != null)
			{
				ancestorCodes.Add(currentCode);
				currentCode = currentCode.ParentCode;
			}

			ancestorCodes.Reverse(); // Must add most removed ancestors first
			ActivityCode[] codesToTryAdd = ancestorCodes.ToArray();

			for (int i = 0; i < codesToTryAdd.Length; i++)
			{
				await AddActivityIfNew(codesToTryAdd[i], isGroup: true);
			}
		}

		private async Task AddActivityIfNew(ActivityCode activityCode, bool isGroup)
		{
			if (await _activityRepository.ActivityExists(activityCode)) return;

			Activity activity = await CreateActivityWithParentRelationship(activityCode, isGroup);

			await _activityRepository.AddActivity(activity);
			await _activityRepository.SaveChanges();
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

		private async Task<Activity> CreateActivityWithParentRelationship(ActivityCode activityCode, bool isGroup)
		{
			if (activityCode == null) throw new ArgumentNullException(nameof(activityCode));

			Activity immediateParent = await _activityRepository.GetActivity(activityCode.ParentCode);

			Activity activity = new()
			{
				IsGroup = isGroup,
				Name = activityCode.ActivityName,
				Parent = immediateParent
			};

			if (immediateParent != null)
			{
				immediateParent.Children.Add(activity);
			}

			return activity;
		}

		private string[] ReadRecordGroupNames(CsvReader csv)
		{
			List<string> groupNames = new();
			for (int i = 0; i < _groupColCount; i++)
			{
				string groupName;
				csv.TryGetField(i, out groupName);

				if (string.IsNullOrWhiteSpace(groupName)) continue;

				groupNames.Add(groupName);
			}
			return groupNames.ToArray();
		}

		private int GetGroupColCount(CsvReader csv)
		{
			return csv.GetFieldIndex("Comment") + 1 - BASE_INTERVAL_COLUMN_COUNT;
		}
	}
}
