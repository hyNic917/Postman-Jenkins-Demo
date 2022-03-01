using dbConsole.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace dbConsole.Data
{
    public class Database
    {
        private readonly IDatabaseConfiguration configuration;

        public Database(IDatabaseConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private string CreateConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = this.configuration.Server,
                InitialCatalog = this.configuration.Database,
                IntegratedSecurity = false, // server auth(false)/win auth(true)
                MultipleActiveResultSets = false, // activate/deactivate MARS
                PersistSecurityInfo = true, // hide login credentials
                UserID = this.configuration.Username, 
                Password = this.configuration.Password 
            };
            return builder.ConnectionString;
        }

        public int CreateBatch(string description)
        {
            using var connection = new SqlConnection(CreateConnectionString());
            connection.Open();
            var sql = "INSERT INTO batch_run(Description) VALUES(@Description); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Description", description);

            var id = cmd.ExecuteScalar();
            return (Int32)id;
        }
    }
}
