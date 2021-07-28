using BTimeLogger.Domain;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	public interface IIntervalsCsvReader
	{
		Task ReadIntervalCsv(string fileLocation);
	}

	class IntervalsCsvReader : IIntervalsCsvReader
	{
		private const int BASE_INTERVAL_COLUMN_COUNT = 5;

		private int _groupColCount;

		private readonly IIntervalRepository _intervalRepository;
		private readonly IActivityRepository _activityRepository;

		public TimedRequeryBehavior RequeryBehavior { get; } = new(TimeSpan.FromSeconds(10));

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
				await ReadRowData(csv);
			}
		}

		public async Task ReadRowData(CsvReader csv)
		{
			InitializeNumColums(csv);

			var record = GetRowRecord(csv);

			await AddNewGroupActivities(record);
			await AddActivityIfNew(record);
			await AddInterval(record);
		}

		private CsvIntervalRecord GetRowRecord(CsvReader csv)
		{
			CsvIntervalRecord record = csv.GetRecord<CsvIntervalRecord>();
			record.Groups = ReadRecordGroupNames(csv);
			return record;
		}

		private async Task AddNewGroupActivities(CsvIntervalRecord record)
		{
			for (int i = 0; i < record.NumGroups; i++)
			{
				string groupName = record.Groups[i];
				if (await _activityRepository.ActivityExists(groupName)) continue;
				string groupParentName = string.Empty;
				if (i > 0)
				{
					string x = record.Groups[i - 1];
					if (x != null) groupParentName = x;
				}

				Activity parent = await _activityRepository.GetActivity(groupParentName);
				Activity group = CreateActivityWithParentRelationship(groupName, parent, isGroup: true);
				await _activityRepository.AddActivity(group);
			}
		}

		private async Task AddActivityIfNew(CsvIntervalRecord record)
		{
			string activityName = record.ActivityType;
			if (await _activityRepository.ActivityExists(activityName)) return;

			string parentName = record.NumGroups > 0
				? record.Groups[record.NumGroups - 1]
				: string.Empty;

			Activity immediateParent = await _activityRepository.GetActivity(parentName);
			var activity = CreateActivityWithParentRelationship(record.ActivityType, immediateParent, isGroup: false);
			await _activityRepository.AddActivity(activity);
		}

		private async Task AddInterval(CsvIntervalRecord record)
		{
			Activity activity = await _activityRepository.GetActivity(record.ActivityType);
			if (activity == null) throw new KeyNotFoundException("Cannot create interval when activity type is not read.");

			DateTime fromDate = CsvDateParser.Parse(record.From);
			DateTime to = CsvDateParser.Parse(record.To);
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

		private Activity CreateActivityWithParentRelationship(string activityName, Activity parent, bool isGroup)
		{
			if (activityName == null) throw new ArgumentNullException(nameof(activityName));

			Activity activity = new()
			{
				Children = Array.Empty<Activity>(),
				IsGroup = isGroup,
				Name = activityName,
				Parent = parent
			};

			if (parent != null)
			{
				List<Activity> parentChildren = parent.Children.ToList();
				parentChildren.Add(activity);
				parent.Children = parentChildren.ToArray();
			}

			return activity;
		}

		private void InitializeNumColums(CsvReader csv)
		{
			_groupColCount = csv.GetFieldIndex("Comment") + 1 - BASE_INTERVAL_COLUMN_COUNT;
		}
	}
}
