using BTimeLogger.Domain;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	interface IIntervalsCsvReader
	{
		Interval[] Intervals { get; }
		Activity[] Activities { get; }

		Task ReadDataAsync();
		void ReadData();
	}

	class IntervalsCsvReader : IIntervalsCsvReader
	{
		private readonly ICsvPrincipal _csvPrincipal;

		public IntervalsCsvReader(ICsvPrincipal csvPrincipal)
		{
			_csvPrincipal = csvPrincipal;
		}

		private readonly Dictionary<string, Activity> _activities = new();
		private readonly List<Interval> _intervals = new();

		public Interval[] Intervals => _intervals.ToArray();

		public Activity[] Activities => _activities.Values.ToArray();

		public Task ReadDataAsync()
		{
			return Task.Factory.StartNew(() => ReadActivityAndIntervalData());
		}

		public void ReadData()
		{
			ReadActivityAndIntervalData();
		}

		private void ReadActivityAndIntervalData()
		{
			if (!File.Exists(_csvPrincipal.IntervalsCsvLocation))
				return;

			using StreamReader reader = new(_csvPrincipal.IntervalsCsvLocation);
			using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
			IntervalRowReader rowReader = new(csv);

			csv.Read();
			csv.ReadHeader();
			while (csv.Read())
			{
				rowReader.ReadRowData();
				AddRowData(rowReader);
			}
		}

		private void AddRowData(IntervalRowReader rowReader)
		{
			_activities[rowReader.ActivityType.Name] = rowReader.ActivityType;
			foreach (var group in rowReader.GroupActivities)
				HandleGroupActivity(group);

			_intervals.Add(rowReader.Interval);
		}

		private void HandleGroupActivity(Activity group)
		{
			if (_activities.ContainsKey(group.Name))
			{
				Activity existingGroup = _activities.GetValueOrDefault(group.Name);
				existingGroup.Children = existingGroup.Children.Union(group.Children, new ActivityNameEqualityOperator());
			}
			else
				_activities.Add(group.Name, group);
		}
	}
}
