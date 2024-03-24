using Appointment.Data;
using Appointment.Repositories;
using Appointment.Services;
using Appointment.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;



namespace Appointment
{
    public class Program
    {
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();
            services.AddSingleton(config);
            services.AddTransient<AppointmentEngine>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog("NLog.config");
            });
            services.AddSingleton<IValidationService, ValidationService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped < IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();

            return services;
        }

        public static IConfiguration LoadConfiguration() 
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return config.Build();
        }

        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
         
            try
            {
                var services = ConfigureServices();

                using (ServiceProvider serviceProvider = services.BuildServiceProvider())
                {
                    serviceProvider.GetService<AppointmentEngine>().Run();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of Exception");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
