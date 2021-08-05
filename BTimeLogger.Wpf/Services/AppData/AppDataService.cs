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
		public bool DataFileExists(string fileName)
		{
			string fileLocation = GetFileLocation(fileName);
			return File.Exists(fileLocation);
		}

		public string GetFileLocation(string fileName)
		{
			string appDataFolder = Environment.GetFolderPath(
									 Environment.SpecialFolder.ApplicationData);
			string filePath = Path.Combine(appDataFolder, fileName);
			return filePath;
		}

		public string GetOrCreate(string fileName)
		{
			string fileLocation = GetFileLocation(fileName);
			if (DataFileExists(fileName)) return fileLocation;

			FileStream fs = File.Create(fileLocation);
			fs.Close();

			return fileLocation;
		}
	}
}
