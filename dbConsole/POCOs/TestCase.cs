using dbConsole.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace dbConsole.POCOs
{
    public class TestCase
    {
        public TestCase(string name, string description, string appExternalId, bool didTestPass, TestCaseStatus status)
        {
            this.Name = name;
            this.Description = description; 
            this.AppExternalId = appExternalId; 
            this.Status = status;
            this.DidTestPass = didTestPass;
        }

        public TestCase(NewmanResult newmanResult)
        {
            this.Name = newmanResult.RequestName;
            string[] parts = newmanResult.RequestDescription.Split('|', StringSplitOptions.RemoveEmptyEntries);
            this.Description = parts[1].Trim();
            this.AppExternalId = parts[0].Trim();
            this.Status = newmanResult.GetTestCaseStatus();
            this.DidTestPass = newmanResult.DidTestPass();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public string AppExternalId { get; private set; }

        /// <summary>
        /// Whether the test passed or not
        /// </summary>
        public TestCaseStatus Status { get; private set; }

        public bool DidTestPass { get; private set; }
    }
}
