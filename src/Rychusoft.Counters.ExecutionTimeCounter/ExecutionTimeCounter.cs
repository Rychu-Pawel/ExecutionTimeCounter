using Rychusoft.Counters.ExecutionTime.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rychusoft.Counters.ExecutionTime
{
    public static class ExecutionTimeCounter
    {
        private static readonly ConcurrentDictionary<string, List<Execution>> executions = new ConcurrentDictionary<string, List<Execution>>();

        public static IReadOnlyDictionary<string, IReadOnlyCollection<Execution>> Executions
            => executions.ToDictionary(e => e.Key, e => (IReadOnlyCollection<Execution>)e.Value);

        public static Execution Start(string sectionName)
        {
            var execution = new Execution(sectionName);
            execution.Start();
            return execution;
        }

        public static void Stop(Execution execution)
        {
            execution.Stop();
            AddExecutionToList(execution);
        }

        private static void AddExecutionToList(Execution execution)
        {
            executions.AddOrUpdate(execution.SectionName, new List<Execution> { execution }, (_, executionsList) =>
            {
                executionsList.Add(execution);
                return executionsList;
            });
        }

        public static void Reset()
        {
            executions.Clear();
        }

        public static List<ExecutionResult> Results()
        {
            return executions
                .OrderBy(execution => execution.Key)
                .Select(execution => new ExecutionResult
                {
                    SectionName = execution.Key,
                    Average = TimeSpan.FromMilliseconds(execution.Value.Average(e => e.Elapsed.TotalMilliseconds)),
                    Fastest = TimeSpan.FromMilliseconds(execution.Value.Min(e => e.Elapsed.TotalMilliseconds)),
                    Slowest = TimeSpan.FromMilliseconds(execution.Value.Max(e => e.Elapsed.TotalMilliseconds)),
                    Median = GetMedian(execution.Value),
                    Executions = execution.Value
                })
                .ToList();
        }

        private static TimeSpan GetMedian(List<Execution> executions)
        {
            if (executions.Count == 0)
                return new TimeSpan();

            if (executions.Count == 1)
                return executions[0].Elapsed;

            var sortedExecutions = executions.OrderBy(e => e.Elapsed.TotalMilliseconds);

            if (executions.Count % 2 == 0)
            {
                int index1 = executions.Count / 2;
                int index2 = index1 - 1;

                var milisecondsMedian = (sortedExecutions.ElementAt(index1).Elapsed.TotalMilliseconds + sortedExecutions.ElementAt(index2).Elapsed.TotalMilliseconds) / 2.0;
                return TimeSpan.FromMilliseconds(milisecondsMedian);
            }

            int medianIndex = (executions.Count - 1) / 2;
            return sortedExecutions.ElementAt(medianIndex).Elapsed;
        }

        public static string ResultsToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var result in Results())
            {
                sb.AppendLine($"{result.SectionName}:");
                sb.AppendLine($"  Average: {result.Average}");
                sb.AppendLine($"  Median: {result.Median}");
                sb.AppendLine($"  Fastest: {result.Fastest}");
                sb.AppendLine($"  Slowest: {result.Slowest}");
                sb.AppendLine($"  Executions: {result.Executions.Count}");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}