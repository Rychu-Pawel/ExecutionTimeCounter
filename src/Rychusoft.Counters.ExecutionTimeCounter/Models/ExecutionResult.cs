using System;
using System.Collections.Generic;

namespace Rychusoft.Counters.ExecutionTime.Models
{
    public class ExecutionResult
    {
        public string SectionName { get; set; }
        public TimeSpan Slowest { get; set; }
        public TimeSpan Average { get; set; }
        public TimeSpan Median { get; set; }
        public TimeSpan Fastest { get; set; }
        public List<Execution> Executions { get; set; }
    }
}