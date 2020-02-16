using NUnit.Framework;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    [TestFixture]
    public class ExecutionTimeCounter_ResultsToString
    {
        [TearDown]
        public void TearDown()
        {
            ExecutionTimeCounter.Reset();
        }
    }
}