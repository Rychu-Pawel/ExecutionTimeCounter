using Rychusoft.Counters.ExecutionTime.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Rychusoft.Counters.ExecutionTime
{
    public static class ExecutionTimeCounter
    {
        private static ConcurrentDictionary<string, List<double>> executions = new ConcurrentDictionary<string, List<double>>();

        public static Execution Start(string sectionName)
        {
            return new Execution(sectionName);
        }

        public static void Stop(Execution execution)
        {

        }

        public static void Reset()
        {
            executions.Clear();
        }

        public static List<ExecutionResult> Results()
        {
            return new List<ExecutionResult>();
        }

        public static string ResultsToString()
        {
            return string.Empty;
        }
    }
}
