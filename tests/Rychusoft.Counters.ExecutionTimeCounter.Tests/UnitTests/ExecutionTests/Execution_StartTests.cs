using NUnit.Framework;
using Rychusoft.Counters.ExecutionTime;
using System;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTimeCounter.Tests.UnitTests.ExecutionTests
{
    [TestFixture]
    public class Execution_StartTests
    {
        [Test]
        public void ShouldStartStopwatch()
        {
            //Arrange
            Execution execution = new Execution("SectionName");

            //Act
            execution.Start();

            Thread.Sleep(1);

            //Assert
            Assert.Greater(execution.Elapsed, new TimeSpan(0));
        }
    }
}