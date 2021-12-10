using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WpfCore.MessageBus;

namespace WpfCore
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add the core components needed for a WPF application to the <paramref name="services"/>
		/// using the provided <paramref name="configuration"/>.
		/// </summary>
		/// <exception cref="ArgumentNullException"/>
		public static IServiceCollection AddWpfCore(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			services.AddSingleton<IEventAggregator, EventAggregator>();
			return services;
		}
	}
}
