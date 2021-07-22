﻿using BTimeLogger.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Csv
{
	class CsvActivityRepository : IActivityRepository
	{
		private readonly ICsvReportReader _reportReader;

		public CsvActivityRepository(ICsvReportReader reportReader)
		{
			_reportReader = reportReader;
		}

		public Task<bool> ActivityExists(string name)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<Activity>> GetActivities()
		{
			// TODO:
			Activity parent = new Activity()
			{
				Children = Array.Empty<Activity>(),
				IsGroup = true,
				Name = "Entertainment",
				Parent = null
			};
			Activity child = new Activity()
			{
				Children = Array.Empty<Activity>(),
				Parent = parent,
				Name = "VR",
				IsGroup = false
			};
			parent.Children = new Activity[] { child };

			return Task.FromResult(new Activity[] { parent, child }.AsQueryable());
		}

		public Task<Activity> GetActivity(string name)
		{
			throw new NotImplementedException();
		}
	}
}
