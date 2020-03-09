using NUnit.Framework;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    [TestFixture]
    public class ExecutionTimeCounter_Reset : ExecutionTimeCounterTestsBase
    {
        [Test]
        public void ShouldClearExecutions()
        {
            //Act
            CreateExecutions(5, 1);

            ExecutionTimeCounter.Reset();

            //Assert
            Assert.AreEqual(0, ExecutionTimeCounter.Executions.Count);
        }
    }
}