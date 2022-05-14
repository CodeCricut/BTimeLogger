using System;
using System.IO;

namespace BTimeLogger.Wpf.Services.AppData;

/// <summary>
/// Interface for working with files in the app data directory.
/// </summary>
public interface IAppDataService
{
	bool DataFileExists(string fileName);
	string GetFileLocation(string fileName);
	string GetOrCreate(string fileName);
}

class AppDataService : IAppDataService
{
	private const string AppDataFolderName = "BTimeLogger";

	/// <summary>
	/// Does a file with the name <paramref name="fileName"/> exist within the app data directory.
	/// </summary>
	/// <param name="fileName"></param>
	/// <returns></returns>
	public bool DataFileExists(string fileName)
	{
		string fileLocation = GetFileLocation(fileName);
		return File.Exists(fileLocation);
	}

	/// <summary>
	/// Get the location of the file in the app data directory, which may or may not exist. 
	/// </summary>
	/// <returns>The location of the file, consisting <paramref name="fileName"/> prefixed by the app data directory path.</returns>
	public string GetFileLocation(string fileName)
	{
		if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException(nameof(fileName));

		string appDataFolder = GetAppDataFolderLocation();
		return Path.Combine(appDataFolder, fileName);
	}

	/// <summary>
	/// If a file with the name <paramref name="fileName"/> exists with the app data directory, return the 
	/// location of the file. Otherwise, create the file and return the location of the new file.
	/// </summary>
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
