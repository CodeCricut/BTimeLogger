using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BTimeLogger.Csv
{
	// TODO: csv should only be read once, perhaps with the option to refresh results
	interface IIntervalsCsvReader
	{
		Interval[] ReadIntervals();
		Activity[] ReadActivities();
	}

	class IntervalsCsvReader : IIntervalsCsvReader
	{
		private const int BASE_INTERVAL_COLUMN_COUNT = 5;
		private readonly ICsvPrincipal _csvPrincipal;

		public IntervalsCsvReader(ICsvPrincipal csvPrincipal)
		{
			_csvPrincipal = csvPrincipal;
		}

		private readonly Dictionary<string, Activity> _activities = new();
		private readonly List<Interval> _intervals = new();

		private int numGroupLvls;

		public Activity[] ReadActivities()
		{
			ReadActivityAndIntervalData();
			return _activities.Values.ToArray();
		}

		public Interval[] ReadIntervals()
		{
			ReadActivityAndIntervalData();
			return _intervals.ToArray();
		}

		private void ReadActivityAndIntervalData()
		{
			AssertFileExists();

			using var reader = new StreamReader(_csvPrincipal.IntervalsCsvLocation);
			using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

			csv.Read();
			csv.ReadHeader();
			numGroupLvls = GetActivityNumColumns(csv);

			while (csv.Read())
				ReadRowActivityAndIntervalData(csv);
		}

		private void ReadRowActivityAndIntervalData(CsvReader csv)
		{
			CsvIntervalRecord record = csv.GetRecord<CsvIntervalRecord>();
			record.Groups = ReadRecordGroupNames(csv);

			AddNewGroupActivitesFromRecord(record);
			AddActivityFromRecord(record);
			AddIntervalFromRecord(record);
		}

		private void AddIntervalFromRecord(CsvIntervalRecord record)
		{
			Interval interval = CreateIntervalFromRecord(record);
			_intervals.Add(interval);
		}

		private Interval CreateIntervalFromRecord(CsvIntervalRecord record)
		{
			Activity intervalActivity = _activities.GetValueOrDefault(record.ActivityType);
			if (intervalActivity == null) throw new KeyNotFoundException("Cannot create interval when activity type is not added to collection.");

			DateTime fromDate = CsvDateParser.Parse(record.From);
			DateTime to = CsvDateParser.Parse(record.To);
			TimeSpan duration = to - fromDate;

			return new Interval()
			{
				Activity = intervalActivity,
				Comment = record.Comment,
				Duration = duration,
				From = fromDate,
				To = to
			};
		}

		private void AddActivityFromRecord(CsvIntervalRecord record)
		{
			if (_activities.ContainsKey(record.ActivityType)) return;

			string parentName = record.NumGroups > 0
				? record.Groups[record.NumGroups - 1]
				: string.Empty;

			Activity immediateParent = _activities.GetValueOrDefault(parentName);
			Activity activity = CreateActivityWithParentRelationship(record.ActivityType, immediateParent, isGroup: false);
			_activities.Add(record.ActivityType, activity);
		}

		private void AddNewGroupActivitesFromRecord(CsvIntervalRecord record)
		{
			for (int i = 0; i < record.NumGroups; i++)
			{
				string groupName = record.Groups[i];

				string groupParentName = string.Empty;
				if (i > 0)
				{
					string x = record.Groups[i - 1];
					if (x != null) groupParentName = x;
				}

				Activity parent = _activities.GetValueOrDefault(groupParentName);

				if (_activities.ContainsKey(groupName)) return;

				Activity group = CreateActivityWithParentRelationship(groupName, parent, isGroup: true);
				_activities.Add(groupName, group);
			}
		}

		private string[] ReadRecordGroupNames(CsvReader csv)
		{
			List<string> groupNames = new();
			for (int i = 0; i < numGroupLvls; i++)
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
			//if (parentName == null) throw new ArgumentNullException(nameof(parentName));

			bool activityAlreadyAdded = _activities.ContainsKey(activityName);

			if (activityAlreadyAdded)
				throw new InvalidOperationException("Tried to create already existing activity.");
			//return activity;

			//_activities.TryGetValue(parentName, out Activity parent);

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

			//_activities.Add(activityName, activity);

			return activity;
			//return true;
		}

		private static int GetActivityNumColumns(CsvReader csv)
		{
			return csv.GetFieldIndex("Comment") + 1 - BASE_INTERVAL_COLUMN_COUNT;
		}

		private void AssertFileExists()
		{
			if (!File.Exists(_csvPrincipal.IntervalsCsvLocation))
				throw new FileNotFoundException("Could not find the CSV file.", _csvPrincipal.IntervalsCsvLocation);
		}
	}
}
