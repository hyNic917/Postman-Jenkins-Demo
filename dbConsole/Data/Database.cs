using dbConsole.Configuration;
using dbConsole.Enums;
using dbConsole.POCOs;
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

        public int GetLastBatchId()
        {
            using var connection = new SqlConnection(CreateConnectionString());
            connection.Open();
            var sql = "SELECT TOP 1 batchId FROM batch_run ORDER BY batchId DESC";
            using var cmd = new SqlCommand(sql, connection);
            var id = cmd.ExecuteScalar();
            return id == null ? 0 : (Int32)id;
        }

        public int CreateTestCase(int batchId, TestCase testCase)
        {
            using var connection = new SqlConnection(CreateConnectionString());
            connection.Open();
            var sql = "INSERT INTO test_case(Name, Description, AppExternalId, Status, BatchId) VALUES(@Name, @Description, @AppExternalId, @Status, @BatchId); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Name", testCase.Name);
            cmd.Parameters.AddWithValue("@Description", testCase.Description);
            cmd.Parameters.AddWithValue("@AppExternalId", testCase.AppExternalId);
            cmd.Parameters.AddWithValue("@Status", testCase.Status.DisplayString());
            cmd.Parameters.AddWithValue("@BatchId", batchId);

            var id = cmd.ExecuteScalar();
            return (Int32)id;
        }

        public int CreateTestCaseItem(int testCaseId, bool didTestPass)
        {
            using var connection = new SqlConnection(CreateConnectionString());
            connection.Open();
            var sql = "INSERT INTO test_case_item(TestCaseId, Result) VALUES(@TestCaseId, @Result); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@TestCaseId", testCaseId);
            cmd.Parameters.AddWithValue("@Result", didTestPass ? "T" : "F");

            var id = cmd.ExecuteScalar();
            return (Int32)id;
        }
    }
}
