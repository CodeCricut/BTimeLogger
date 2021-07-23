using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BTimeLogger.Csv
{
	// TODO: csv should only be read once, perhaps with the option to refresh results
	interface ICsvReportReader
	{
		Interval[] ReadIntervals();
		Activity[] ReadActivities();
		Statistic[] ReadStatistics();
	}

	class CsvReportReader : ICsvReportReader
	{
		private const int BASE_INTERVAL_COLUMN_COUNT = 5;
		private readonly ICsvPrincipal _csvPrincipal;

		public CsvReportReader(ICsvPrincipal csvPrincipal)
		{
			_csvPrincipal = csvPrincipal;
		}


		private Dictionary<string, Activity> Activities = new();


		private Dictionary<string, Activity> Intervals = new();
		private int numGroupLvls;

		public Activity[] ReadActivities()
		{
			AssertFileExists();

			using var reader = new StreamReader(_csvPrincipal.CsvFileLocation);
			using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

			csv.Read();
			csv.ReadHeader();
			numGroupLvls = GetActivityNumColumns(csv);


			bool moreIntervalRows = true;
			while (csv.Read() && moreIntervalRows)
			{
				try
				{
					ReadActivityRow(csv);
				}
				catch (Exception e)
				{
					moreIntervalRows = false;
				}

			}

			return Activities.Values.ToArray();
		}

		private void ReadActivityRow(CsvReader csv)
		{
			CsvIntervalRecord record = csv.GetRecord<CsvIntervalRecord>();
			record.Groups = ReadRecordGroupNames(csv);
			AddRecordGroupActivities(record);
			AddRecordActivity(record);
		}

		private void AddRecordActivity(CsvIntervalRecord record)
		{
			string parentName = string.Empty;
			if (record.NumGroups > 0)
				parentName = record.Groups[record.NumGroups - 1];
			TryAddActivity(record.ActivityType, isGroup: false, parentName);
		}

		private void AddRecordGroupActivities(CsvIntervalRecord record)
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

				TryAddActivity(groupName, isGroup: true, groupParentName);
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

		private bool TryAddActivity(string activityName, bool isGroup = false, string parentName = "")
		{
			if (activityName == null) throw new ArgumentNullException(nameof(activityName));
			if (parentName == null) throw new ArgumentNullException(nameof(parentName));

			Activity activity;
			bool activityAlreadyAdded = Activities.TryGetValue(activityName, out activity);

			if (activityAlreadyAdded) return false;

			Activities.TryGetValue(parentName, out Activity parent);

			activity = new Activity()
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

			Activities.Add(activityName, activity);

			return true;
		}

		private static int GetActivityNumColumns(CsvReader csv)
		{
			return csv.GetFieldIndex("Comment") + 1 - BASE_INTERVAL_COLUMN_COUNT;
		}

		public Interval[] ReadIntervals()
		{
			throw new NotImplementedException();
		}

		public Statistic[] ReadStatistics()
		{
			throw new System.NotImplementedException();
		}

		private void AssertFileExists()
		{
			if (!File.Exists(_csvPrincipal.CsvFileLocation))
				throw new FileNotFoundException("Could not find the CSV file.", _csvPrincipal.CsvFileLocation);
		}
	}
}
