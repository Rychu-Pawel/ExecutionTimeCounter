using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Rychusoft.Counters.ExecutionTime.Models
{
    public class Execution
    {
        private readonly Stopwatch sw;

        public string SectionName { get; }
        public TimeSpan Elapsed => sw.Elapsed;

        public Execution(string sectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentNullException(nameof(sectionName));

            this.sw = new Stopwatch();
            this.SectionName = sectionName;
        }

        public void Start()
        {
            sw.Start();
        }

        public void Stop()
        {
            ExecutionTimeCounter.Stop(this);
        }
    }
}
