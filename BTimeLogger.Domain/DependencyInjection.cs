using BTimeLogger.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTimeLogger.Domain
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<IActivityRepository, ActivityRepository>();
			services.AddSingleton<IIntervalRepository, IntervalRepository>();
			services.AddSingleton<IStatisticsRepository, StatisticsRepository>();
			services.AddSingleton<IGroupStatisticRepository, GroupStatisticRepository>();

			return services;
		}
	}
}
