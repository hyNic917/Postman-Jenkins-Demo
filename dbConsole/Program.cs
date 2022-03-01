using dbConsole.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;

namespace dbConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices(args[0], args[1]);

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run(args[0], args[1]);
        }

        private static IServiceCollection ConfigureServices(string username, string password)
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration(username, password);
            services.AddSingleton(config);

            services.AddScoped<IDatabaseConfiguration, DatabaseConfiguration>();
            // required to run the application
            services.AddTransient<App>();
            

            return services;
        }

        public static IConfiguration LoadConfiguration(string username, string password)
        {
            string json = $"{{\"DatabaseSettings\": {{\"Username\": \"{username}\",\"Password\":  \"{password}\"}}}}";
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName ?? "Production"}.json", optional: false, reloadOnChange: true)
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .AddUserSecrets<App>();

            return builder.Build();
        }        
    }
}
