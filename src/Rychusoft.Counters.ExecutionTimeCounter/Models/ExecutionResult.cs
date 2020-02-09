using System;
using System.Collections.Generic;
using System.Text;

namespace Rychusoft.Counters.ExecutionTime.Models
{
    public class ExecutionResult
    {
        public string SectionName { get; set; }
        public TimeSpan Min { get; set; }
        public TimeSpan Avg { get; set; }
        public TimeSpan Max { get; set; }
        public long Executions { get; set; }
    }
}
