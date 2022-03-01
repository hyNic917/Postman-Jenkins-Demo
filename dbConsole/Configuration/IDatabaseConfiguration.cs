using System;
using System.Collections.Generic;
using System.Text;

namespace dbConsole.Configuration
{
    public interface IDatabaseConfiguration
    {
        public string Server { get; }

        public string Database { get; }

        public string Username { get; }

        public string Password { get; }
    }
}
