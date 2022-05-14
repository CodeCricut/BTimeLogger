using BTimeLogger.Csv;
using BTimeLogger.Wpf.Services;
using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;

namespace BTimeLogger.Wpf.Configuration;

public static class DependencyInjection
{
	public static IServiceCollection AddWpf(this IServiceCollection services, IConfiguration configuration)
	{
		if (services == null) throw new ArgumentNullException(nameof(services));
		if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		services.Configure<ReportLocationsDataFileSettings>(opts =>
			configuration.GetSection(ReportLocationsDataFileSettings.ReportLocationsDataFile)
			.Bind(opts));

		return services
			.AddSingleton<ISkinManager, SkinManager>()

			.AddSingleton<IAppDataService, AppDataService>()
			.AddSingleton<IReportLocationsPrincipal, ReportLocationsPrincipal>()

			.AddSingleton<IStatisticCategoryConverter, StatisticCategoryConverter>()

			.AddSingleton<ICsvChangeTracker, CsvChangeTracker>()


			.RegisterAllViewModelFactories()

			.AddMemoryCache()

			.AddMediatR(typeof(App).Assembly);
	}


	private static IServiceCollection RegisterAllViewModelFactories(this IServiceCollection services)
		=> services.Scan(scan =>
				scan.FromCallingAssembly()
					.AddClasses(c => c.GetType().Name.Contains("ViewModelFactory"))
					.UsingRegistrationStrategy(RegistrationStrategy.Skip)
					.AsMatchingInterface()
					.WithSingletonLifetime());


}
