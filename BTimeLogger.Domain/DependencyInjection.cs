using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTimeLogger.Domain
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration config)
		{
			//services.AddSingleton<IActivityReporter, ActivityReporter>();
			//services.AddSingleton<IIntervalsReporter, IntervalsReporter>();
			//services.AddSingleton<IStatisticsReporter, StatisticsReporter>();

			return services;
		}
	}
}
