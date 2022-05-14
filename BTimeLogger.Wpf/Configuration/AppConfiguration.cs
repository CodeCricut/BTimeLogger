using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BTimeLogger.Wpf.Configuration;

public static class AppConfiguration
{
	/// <summary>
	/// Create the <see cref="IConfiguration"/> object for the services used by the DI container.
	/// </summary>
	public static IConfiguration GetServiceConfiguration()
	{
		var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json",
				optional: true, reloadOnChange: true)
			.Build();

		return config;
	}
}
