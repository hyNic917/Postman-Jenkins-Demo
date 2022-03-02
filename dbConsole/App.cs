using CsvHelper;
using CsvHelper.Configuration;
using dbConsole.Configuration;
using dbConsole.Data;
using dbConsole.POCOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace dbConsole
{
    internal class App
    {
        private readonly Database database;

        public App(IDatabaseConfiguration config)
        {
            this.database = new Database(config);
        }

        public void Run()
        {
            Console.WriteLine("Hello from App.cs");

            int previousBatchId = this.database.GetLastBatchId();
            string description = "Test " + ++previousBatchId;
            int currentBatchid = this.database.CreateBatch(description);
            Console.WriteLine($"Working with batch {currentBatchid}");

            int count = 0;
            foreach(NewmanResult result in LoadResults())
            {
                count++;
                Console.WriteLine($"Started processing result {count}");

                TestCase testCase = new TestCase(result);
                int testCaseId = this.database.CreateTestCase(currentBatchid,testCase);
                this.database.CreateTestCaseItem(testCaseId, testCase.DidTestPass);

                Console.WriteLine($"Finished processing result {count}");
            }
        }

        /// <summary>
        /// Loads the Postman results file.
        /// </summary>
        /// <returns>The result records.</returns>
        private static IEnumerable<NewmanResult> LoadResults()
        {
            DirectoryInfo binDirectory = System.IO.Directory.GetParent(Environment.CurrentDirectory);
            DirectoryInfo reportDirectory = new DirectoryInfo(Path.Combine(binDirectory.Parent.Parent.Parent.Parent.ToString(), "scor-tci-writer", "newman"));
            var file = reportDirectory.GetFiles().Where(f => f.Name.EndsWith(".csv")).OrderByDescending(f => f.LastWriteTime).First();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
            };
            using var reader = new StreamReader(file.FullName);
            using var csv = new CsvReader(reader, config);
            return csv.GetRecords<NewmanResult>().ToList();
        }
    }
}
