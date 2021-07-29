using BTimeLogger.Wpf.Services;
using BTimeLogger.Wpf.Services.ViewManagement;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Windows;

namespace BTimeLogger.Wpf.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWpf(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));


			return services
				.AddSingleton<IGroupStatisticCategoriesConverter, GroupStatisticCategoriesConverter>()

				.AddSingleton<IViewFinderFactory, ViewFinderFactory>()

				.AddSingleton<IViewManagerFactory, ViewManagerFactory>()
				.RegisterViewManager()

				.RegisterAllViewModelFactories()

				.RegisterAllWindows()

				.AddMemoryCache()

				.AddMediatR(typeof(App).Assembly);
		}

		/// <summary>
		/// Register a <see cref="IViewManager"/> to the service collection, providing it with the current service provider
		/// when it is requested.
		/// </summary>
		private static IServiceCollection RegisterViewManager(this IServiceCollection services)
			=> services.AddSingleton(sp =>
			{
				var viewManagerFactory = sp.GetRequiredService<IViewManagerFactory>();
				return viewManagerFactory.CreateWithServices(sp);
			});

		private static IServiceCollection RegisterAllViewModelFactories(this IServiceCollection services)
			=> services.Scan(scan =>
					scan.FromCallingAssembly()
						.AddClasses(c => c.GetType().Name.Contains("ViewModelFactory"))
						.UsingRegistrationStrategy(RegistrationStrategy.Skip)
						.AsMatchingInterface()
						.WithSingletonLifetime());

		private static IServiceCollection RegisterAllWindows(this IServiceCollection services)
			=> services.Scan(scan =>
					scan.FromCallingAssembly()
						.AddClasses(c => c.AssignableTo<Window>())
						.UsingRegistrationStrategy(RegistrationStrategy.Skip)
						.AsSelf()
						.WithTransientLifetime());
	}
}
