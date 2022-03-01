using dbConsole.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace dbConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            foreach(var arg in args)
            {
                Console.WriteLine($"Arg[{i}]: {arg}");
                i++;
            }
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);
            services.AddScoped<IDatabaseConfiguration, DatabaseConfiguration>();
            // required to run the application
            services.AddTransient<App>();
            

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName ?? "Production"}.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<App>();

            return builder.Build();
        }        
    }
}
