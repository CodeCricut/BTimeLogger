using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTimeLogger.Csv
{
	public class IntervalRowReader
	{
		private const int BASE_INTERVAL_COLUMN_COUNT = 5;

		private readonly CsvReader _csv;

		private int _groupColCount;

		public CsvIntervalRecord Record { get; private set; }
		public Activity ActivityType { get; private set; }
		public List<Activity> GroupActivities { get; private set; } = new();
		public Interval Interval { get; private set; }

		public IntervalRowReader(CsvReader csv)
		{
			_csv = csv;
		}

		public void ReadRowData()
		{
			ResetRowData();
			InitializeNumColums();

			Record = GetRowRecord();

			GroupActivities.AddRange(
				GetGroupActivities()
				);

			ActivityType = GetActivityType();

			Interval = GetInterval();
		}

		private void ResetRowData()
		{
			Record = null;
			ActivityType = null;
			GroupActivities.Clear();
			Interval = null;
		}

		private CsvIntervalRecord GetRowRecord()
		{
			CsvIntervalRecord record = _csv.GetRecord<CsvIntervalRecord>();
			record.Groups = ReadRecordGroupNames();
			return record;
		}

		private IEnumerable<Activity> GetGroupActivities()
		{
			Dictionary<string, Activity> groups = new();
			for (int i = 0; i < Record.NumGroups; i++)
			{
				string groupName = Record.Groups[i];
				if (groups.ContainsKey(groupName)) continue;

				string groupParentName = string.Empty;
				if (i > 0)
				{
					string x = Record.Groups[i - 1];
					if (x != null) groupParentName = x;
				}

				Activity parent = groups.GetValueOrDefault(groupParentName);

				Activity group = CreateActivityWithParentRelationship(groupName, parent, isGroup: true);
				groups.Add(groupName, group);
			}
			return groups.Values.AsEnumerable();
		}

		private Activity GetActivityType()
		{
			string parentName = Record.NumGroups > 0
				? Record.Groups[Record.NumGroups - 1]
				: string.Empty;

			Activity immediateParent = GroupActivities.FirstOrDefault(group => group.Name.Equals(parentName));
			return CreateActivityWithParentRelationship(Record.ActivityType, immediateParent, isGroup: false);
		}

		private Interval GetInterval()
		{
			if (ActivityType == null) throw new KeyNotFoundException("Cannot create interval when activity type is not read.");

			DateTime fromDate = CsvDateParser.Parse(Record.From);
			DateTime to = CsvDateParser.Parse(Record.To);
			TimeSpan duration = to - fromDate;

			return new Interval()
			{
				Activity = ActivityType,
				Comment = Record.Comment,
				Duration = duration,
				From = fromDate,
				To = to
			};
		}

		private string[] ReadRecordGroupNames()
		{
			List<string> groupNames = new();
			for (int i = 0; i < _groupColCount; i++)
			{
				string groupName;
				_csv.TryGetField(i, out groupName);

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

		private void InitializeNumColums()
		{
			_groupColCount = _csv.GetFieldIndex("Comment") + 1 - BASE_INTERVAL_COLUMN_COUNT;
		}
	}
}
