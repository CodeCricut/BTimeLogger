using BTimeLogger.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTimeLogger.Domain;

public static class DependencyInjection
{
	public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration config)
	{
		services.AddSingleton<IActivityRepository, ActivityRepository>();
		services.AddSingleton<IIntervalRepository, IntervalRepository>();
		services.AddSingleton<IGroupStatisticGenerator, GroupStatisticGenerator>();

		services.AddSingleton<IStatisticsGenerator, StatisticsGenerator>();

		return services;
	}
}
