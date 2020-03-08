using NUnit.Framework;
using System.Linq;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    [TestFixture]
    public class ExecutionTimeCounter_Stop : ExecutionTimeCounterTestsBase
    {
        [Test]
        public void ShouldStopStopwatch()
        {
            //Arrange & Act
            var execution = ExecutionTimeCounter.Start("SectionName");

            ExecutionTimeCounter.Stop(execution);

            var elapsed = execution.Elapsed;

            Thread.Sleep(1);

            //Assert
            Assert.AreEqual(elapsed, execution.Elapsed);
        }

        [Test]
        public void ShouldCreateNewExecutionsEntryForEveryNewSectionName()
        {
            //Arrange
            int iterations = 5;

            //Act
            CreateExecutions(iterations, 1);

            //Assert
            Assert.AreEqual(iterations, ExecutionTimeCounter.Executions.Keys.Count());
        }

        [Test]
        public void ShouldUpdateExecutionsEntryForExistingSectionNames()
        {
            //Arrange
            int iterations = 5;

            //Act
            CreateExecutions(iterations, 1);

            //Assert
            foreach (var key in ExecutionTimeCounter.Executions.Keys)
                Assert.AreEqual(iterations, ExecutionTimeCounter.Executions[key].Count);
        }
    }
}