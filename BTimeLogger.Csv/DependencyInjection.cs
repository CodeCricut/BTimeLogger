using BTimeLogger.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTimeLogger.Csv
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCsv(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<ICsvPrincipal, CsvPrincipal>();
			services.AddSingleton<ICsvReportReader, CsvReportReader>();

			services.AddSingleton<IActivityRepository, CsvActivityRepository>();
			services.AddSingleton<IIntervalRepository, CsvIntervalRepository>();
			services.AddSingleton<IStatisticsRepository, CsvStatisticRepository>();

			return services;
		}
	}
}
