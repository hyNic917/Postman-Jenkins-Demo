using CsvHelper;
using dbConsole.Configuration;
using dbConsole.Data;
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

        public void Run(string username, string password)
        {
            Console.WriteLine("Hello from App.cs");
            try
            {
                int i = this.database.CreateBatch("Test 1");
                Console.WriteLine($"Working with batch {i}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //foreach(NewmanResult result in ReadFile())
            //{

            //}
        }

        /// <summary>
        /// Loads the Postman results file.
        /// </summary>
        /// <returns>The result records.</returns>
        private static IEnumerable<NewmanResult> LoadResults()
        {
            DirectoryInfo binDirectory = System.IO.Directory.GetParent(Environment.CurrentDirectory);
            DirectoryInfo reportDirectory = new DirectoryInfo(Path.Combine(binDirectory.Parent.Parent.Parent.ToString(), "scor-tci-writer", "newman"));
            var file = reportDirectory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();

            using var reader = new StreamReader(file.FullName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<NewmanResult>();
        }
    }
}
