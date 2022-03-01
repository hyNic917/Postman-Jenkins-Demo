using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace dbConsole.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {

        private const string Section = "DatabaseSettings";
        public DatabaseConfiguration(IConfiguration configuration)
        {
            var section = configuration.GetSection(Section);
            this.Server =section.GetValue<string>("Server");
            this.Database = section.GetValue<string>("Database");
            this.Username = section.GetValue<string>("Username");
            this.Password = section.GetValue<string>("Password");
            Console.WriteLine($"Setup db configuration. Server: {this.Server}, Database: {this.Database}, User: {this.Username}");
        }
        public string Server { get; }

        public string Database { get; }

        public string Username { get; }

        public string Password { get; }
    }
}
