using NUnit.Framework;
using Rychusoft.Counters.ExecutionTime.Exceptions;
using System;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTests
{
    [TestFixture]
    public class Execution_StartTests
    {
        private Execution execution;

        [SetUp]
        public void SetUp()
        {
            execution = new Execution("SectionName");
        }

        [Test]
        public void ShouldStartStopwatch()
        {
            //Act
            execution.Start();

            Thread.Sleep(1);

            //Assert
            Assert.Greater(execution.Elapsed, new TimeSpan(0));
        }

        [Test]
        public void ShouldSetStartedDate()
        {
            //Act
            execution.Start();

            //Assert
            Assert.LessOrEqual(execution.Started, DateTime.Now);
            Assert.GreaterOrEqual(execution.Started, DateTime.Now.AddMinutes(-1));
        }

        [Test]
        public void ShouldThrowWhenStartedAgain()
        {
            //Arrange
            execution.Start();

            //Act & Assert
            Assert.Throws<ExecutionAlreadyStartedException>(() => execution.Start());
        }
    }
}