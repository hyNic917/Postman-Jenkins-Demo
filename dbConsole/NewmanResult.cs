using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace dbConsole
{
    public class NewmanResult
    {
        [Name("iteration")]
        public int Iteration { get; set;  }

        [Name("collection")]
        public string Collection { get; set; }

        [Name("requestName")]
        public string RequestName { get; set; }

        [Name("method")]
        public string Method { get; set; }

        [Name("url")]
        public string Url { get; set; }

        [Name("status")]
        public string Status { get; set; }

        [Name("code")]
        public string Code { get; set; }

        [Name("responseTime")]
        public string ResponseTime { get; set; }

        [Name("responseSize")]
        public string ResponseSize { get; set; }

        [Name("executed")]
        public string Executed { get; set; }

        [Name("failed")]
        public string Failed { get; set; }

        [Name("skipped")]
        public string Skipped { get; set; }
    }
}
