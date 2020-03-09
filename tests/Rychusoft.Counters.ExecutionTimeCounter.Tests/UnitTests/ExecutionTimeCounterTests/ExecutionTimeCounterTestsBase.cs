using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    public abstract class ExecutionTimeCounterTestsBase
    {
        protected readonly Dictionary<string, List<double>> executionsDictionary = new Dictionary<string, List<double>>();
        
        [TearDown]
        public void TearDown()
        {
            ExecutionTimeCounter.Reset();
            executionsDictionary.Clear();
        }

        protected void CreateExecutions(int iterations, int sleepTime)
        {
            for (int i = 0; i < iterations; i++)
            {
                if (!executionsDictionary.ContainsKey(i.ToString()))
                    executionsDictionary.Add(i.ToString(), new List<double>());

                for (int j = 0; j < iterations; j++)
                {
                    var execution = ExecutionTimeCounter.Start(i.ToString());
                    Thread.Sleep(sleepTime);
                    ExecutionTimeCounter.Stop(execution);

                    executionsDictionary[i.ToString()].Add(execution.Elapsed.TotalMilliseconds);
                }
            }
        }
    }
}