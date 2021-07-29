using BTimeLogger.Domain;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Csv
{
	public abstract class ReportCsvReader
	{

		private readonly IIntervalRepository _intervalRepository;
		private readonly IActivityRepository _activityRepository;
		private readonly IStatisticsRepository _statisticsRepository;

		private int _groupColCount;

		public ReportCsvReader(IIntervalRepository intervalRepository,
			IActivityRepository activityRepository,
			IStatisticsRepository statisticsRepository)
		{
			_intervalRepository = intervalRepository;
			_activityRepository = activityRepository;
			_statisticsRepository = statisticsRepository;
		}

		public async Task ReadCsv(string csvLocation)
		{
			using StreamReader reader = new(csvLocation);
			using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

			csv.Read();
			csv.ReadHeader();
			while (csv.Read())
			{
				InitializeNumColums(csv);

				await ReadRowData(csv);
			}
		}

		public abstract Task ReadRowData(CsvReader csv);

		protected async Task AddNewGroupActivities(ActivityCode groupCode)
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

		protected async Task AddActivityIfNew(ActivityCode activityCode, bool isGroup)
		{
			if (await _activityRepository.ActivityExists(activityCode)) return;

			Activity activity = await CreateActivityWithParentRelationship(activityCode, isGroup);

			await _activityRepository.AddActivity(activity);
		}

		protected string[] GetGroupsMinusFirstAncestor(string[] groupNames)
		{
			if (groupNames.Length <= 0) return Array.Empty<string>();
			return groupNames.TakeLast(groupNames.Length - 1).ToArray(); ;
		}

		protected string GetFirstAncestorNameOrEmpty(string[] groupNames)
		{
			if (groupNames.Length <= 0) return string.Empty;
			else return groupNames[groupNames.Length - 1];
		}

		protected async Task<Activity> CreateActivityWithParentRelationship(ActivityCode activityCode, bool isGroup)
		{
			if (activityCode == null) throw new ArgumentNullException(nameof(activityCode));

			Activity immediateParent = await _activityRepository.GetActivity(activityCode.ParentCode);

			Activity activity = new()
			{
				Children = Array.Empty<Activity>(),
				IsGroup = isGroup,
				Name = activityCode.ActivityName,
				Parent = immediateParent
			};

			if (immediateParent != null)
			{
				List<Activity> parentChildren = immediateParent.Children.ToList();
				parentChildren.Add(activity);
				immediateParent.Children = parentChildren.ToArray();
			}

			return activity;
		}

		protected string[] ReadRecordGroupNames(CsvReader csv)
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


		private void InitializeNumColums(CsvReader csv)
		{
			_groupColCount = GetGroupColCount(csv);
		}

		protected abstract int GetGroupColCount(CsvReader csv);
	}
}
