using BTimeLogger.Csv;
using BTimeLogger.Domain;
using BTimeLogger.Wpf.Configuration;
using BTimeLogger.Wpf.Mediator;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfCore;

namespace BTimeLogger.Wpf
{
	/// <summary>
	/// Encapsulates startup logic for this WPF app.
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// The services provider for this app.
		/// </summary>
		public IServiceProvider Services { get; init; }

		public App()
		{
			Services = BuildServiceProvider();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			OpenLoginWindow();
		}

		private IServiceProvider BuildServiceProvider()
		{
			var services = new ServiceCollection();
			var config = AppConfiguration.GetServiceConfiguration();

			ConfigureServices(services, config);

			return services.BuildServiceProvider();
		}

		private static void ConfigureServices(IServiceCollection services, IConfiguration config)
		{
			services.AddDomain(config);
			services.AddCsv(config);
			services.AddWpfCore(config);
			services.AddWpf(config);
		}

		private void OpenLoginWindow()
		{
			Services.GetRequiredService<IMediator>()
				.Send(new OpenMainWindowRequest())
				.GetAwaiter().GetResult();
		}
	}
}
