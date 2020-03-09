using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    [TestFixture]
    public class ExecutionTimeCounter_Start
    {
        [TearDown]
        public void TearDown()
        {
            ExecutionTimeCounter.Reset();
        }

        [Test]
        public void ShouldReturnNotEmptyExecution()
        {
            //Arrange & Act
            var execution = ExecutionTimeCounter.Start("SectionName");

            //Assert
            Assert.IsNotNull(execution);
        }

        [TestCase("SectionName")]
        [TestCase("SectionName2")]
        [TestCase("qwerty")]
        public void ShouldInitializeExecutionWithCorrectSectionName(string sectionName)
        {
            //Arrange & Act
            var execution = ExecutionTimeCounter.Start(sectionName);

            //Assert
            Assert.AreEqual(sectionName, execution.SectionName);
        }

        [Test]
        public void ShouldReturnStartedExecution()
        {
            //Arrange & Act
            var execution = ExecutionTimeCounter.Start("SectionName");

            Thread.Sleep(1);

            //Assert
            Assert.Greater(execution.Elapsed, new TimeSpan(0));
        }

        [Test]
        public void ShouldAlwaysReturnNewExecution()
        {
            //Arrange
            var iterations = 5;
            var executions = new List<Execution>();

            //Act
            for (int i = 0; i < iterations; i++)
                executions.Add(ExecutionTimeCounter.Start("SectionName"));

            //Assert
            for (int i = 0; i < iterations; i++)
                Assert.AreEqual(1, executions.Count(e => e == executions[i]));
        }
    }
}