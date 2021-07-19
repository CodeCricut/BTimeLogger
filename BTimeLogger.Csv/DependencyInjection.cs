using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTimeLogger.Csv
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCsv(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<ICsvReportReader, CsvReportReader>();
			// TODO: we need to register CsvReportReader as IActivityRepository, but can't pass in csv report path arg.
			// We could make a factory of course, but classed dependent on IActivityRepository couldn't be constructed
			// We could instead have some service responsible for storing the file location, such that
			// it could be injected into CsvReportReader

			return services;
		}
	}
}
