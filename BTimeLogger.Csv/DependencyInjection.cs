using BTimeLogger.Csv.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTimeLogger.Csv
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCsv(this IServiceCollection services, IConfiguration config)
		{
			return services
				.AddSingleton<IIntervalsCsvReader, IntervalsCsvReader>()
				.AddSingleton<ICsvLocationPrincipal, CsvLocationPrincipal>()
				.AddSingleton<IIntervalsCsvWriter, IntervalsCsvWriter>();
		}
	}
}
