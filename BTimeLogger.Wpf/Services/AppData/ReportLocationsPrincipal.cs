using BTimeLogger.Wpf.Configuration;
using BTimeLogger.Wpf.Util;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BTimeLogger.Wpf.Services.AppData
{
	public interface IReportLocationsPrincipal
	{
		void AddReportLocation(string reportLocation);
		void RemoveReportLocation(string reportLocation);

		IEnumerable<string> GetReportLocations();

		void ClearReportLocations();
	}

	class ReportLocationsPrincipal : IReportLocationsPrincipal
	{
		private readonly IAppDataService _appDataService;
		private readonly ReportLocationsDataFileSettings _dataFileSettings;

		public ReportLocationsPrincipal(IAppDataService appDataService, IOptions<ReportLocationsDataFileSettings> dataFileSettings)
		{
			_appDataService = appDataService;
			_dataFileSettings = dataFileSettings.Value;

			if (_dataFileSettings == null || string.IsNullOrWhiteSpace(_dataFileSettings.DataFileName))
				throw new ReportLocationsDataFileNotFoundException();
		}

		public void AddReportLocation(string reportLocation)
		{
			if (string.IsNullOrWhiteSpace(reportLocation)) throw new ArgumentException(nameof(reportLocation));

			string dataFileLocation = GetFileLocation();
			if (ReportLocationAlreadyAdded(reportLocation)) return;

			FileUtil.AppendLine(dataFileLocation, reportLocation);
		}

		public void ClearReportLocations()
		{
			string dataFileLocation = GetFileLocation();
			File.Delete(dataFileLocation);
		}

		public IEnumerable<string> GetReportLocations()
		{
			string dataFileLocation = GetFileLocation();
			return File.ReadAllLines(dataFileLocation);
		}

		public void RemoveReportLocation(string reportLocation)
		{
			if (string.IsNullOrWhiteSpace(reportLocation)) throw new ArgumentException(nameof(reportLocation));

			string dataFileLocation = GetFileLocation();
			RemoveLineWithValue(dataFileLocation, reportLocation);
		}

		private void RemoveLineWithValue(string fileLocation, string value)
		{
			if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentException(nameof(fileLocation));
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(nameof(value));

			IEnumerable<string> reportLocations = GetReportLocations();

			IEnumerable<string> newReportLocations = reportLocations.Where(str => !str.Equals(value));

			File.WriteAllLines(fileLocation, newReportLocations); // Overwrites existing data
		}

		private bool ReportLocationAlreadyAdded(string reportLocation)
		{
			if (string.IsNullOrWhiteSpace(reportLocation)) throw new ArgumentException(nameof(reportLocation));

			return GetReportLocations().Any(loc => loc.Equals(reportLocation));
		}

		private string GetFileLocation()
		{
			return _appDataService.GetOrCreate(_dataFileSettings.DataFileName);
		}
	}
}
