using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    public abstract class ExecutionTimeCounterTestsBase
    {
        protected void CreateExecutions(int iterations, int sleepTime)
        {
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < iterations; j++)
                {
                    var execution = ExecutionTimeCounter.Start(i.ToString());
                    Thread.Sleep(sleepTime);
                    ExecutionTimeCounter.Stop(execution);
                }
            }
        }
    }
}