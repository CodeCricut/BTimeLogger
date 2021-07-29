﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTimeLogger.Csv
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCsv(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<IIntervalsCsvReader, IntervalsCsvReader>();
			services.AddSingleton<IStatisticsCsvReader, StatisticsCsvReader>();

			return services;
		}
	}
}