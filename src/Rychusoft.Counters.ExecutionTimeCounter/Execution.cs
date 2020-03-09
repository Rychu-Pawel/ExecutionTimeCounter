using Rychusoft.Counters.ExecutionTime.Exceptions;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Rychusoft.Counters.ExecutionTime.Tests")]

namespace Rychusoft.Counters.ExecutionTime
{
    public class Execution
    {
        private readonly Stopwatch sw;

        public string SectionName { get; }
        public DateTime Started { get; private set; }
        public TimeSpan Elapsed => sw.Elapsed;

        internal Execution(string sectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentNullException(nameof(sectionName));

            this.sw = new Stopwatch();
            this.SectionName = sectionName;
            this.Started = new DateTime(0, DateTimeKind.Local);
        }

        internal void Start()
        {
            if (sw.IsRunning)
                throw new ExecutionAlreadyStartedException();

            Started = DateTime.Now;
            sw.Start();
        }

        internal void Stop()
        {
            if (!sw.IsRunning)
                throw new ExecutionIsNotRunningException();

            sw.Stop();
        }
    }
}