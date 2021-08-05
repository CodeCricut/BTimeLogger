using System;
using System.IO;

namespace BTimeLogger.Wpf.Services.AppData
{
	public interface IAppDataService
	{
		bool DataFileExists(string fileName);
		string GetFileLocation(string fileName);
		string GetOrCreate(string fileName);
	}

	class AppDataService : IAppDataService
	{
		private const string AppDataFolderName = "BTimeLogger";

		public bool DataFileExists(string fileName)
		{
			string fileLocation = GetFileLocation(fileName);
			return File.Exists(fileLocation);
		}

		public string GetFileLocation(string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException(nameof(fileName));

			string appDataFolder = GetAppDataFolderLocation();
			return Path.Combine(appDataFolder, fileName);
		}

		public string GetOrCreate(string fileName)
		{
			string fileLocation = GetFileLocation(fileName);
			if (DataFileExists(fileName)) return fileLocation;

			EnsureDataFolderCreated();

			FileStream fs = File.Create(fileLocation);
			fs.Close();

			return fileLocation;
		}

		private string GetAppDataFolderLocation()
		{
			string dataFolder = Environment.GetFolderPath(
									 Environment.SpecialFolder.ApplicationData);
			return Path.Combine(dataFolder, AppDataFolderName);
		}

		private void EnsureDataFolderCreated()
		{
			string dataFolderLocation = GetAppDataFolderLocation();
			if (!Directory.Exists(dataFolderLocation)) Directory.CreateDirectory(dataFolderLocation);
		}
	}
}
