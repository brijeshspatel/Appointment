using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureIConfigurationProvider(this IServiceCollection services, string basePath)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var providers = new List<IConfigurationProvider>();
            foreach (var descriptor in services.Where(descriptor => descriptor.ServiceType == typeof(IConfiguration)).ToList())
            {
                var existingConfiguration = descriptor.ImplementationInstance as IConfigurationRoot;

                if (existingConfiguration is null)
                {
                    continue;
                }

                providers.AddRange(existingConfiguration.Providers);

                services.Remove(descriptor);
            }

            providers.AddRange(config.Providers);
            services.AddSingleton<IConfiguration>(new ConfigurationRoot(providers));


            return services;
        }
    }
}
