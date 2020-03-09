using NUnit.Framework;
using Rychusoft.Counters.ExecutionTime.Exceptions;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTests
{
    [TestFixture]
    public class Execution_StopTests
    {
        private Execution execution;

        [SetUp]
        public void SetUp()
        {
            execution = new Execution("SectionName");
        }

        [Test]
        public void ShouldThrowWhenNotStarted()
        {
            //Act & Assert
            Assert.Throws<ExecutionIsNotRunningException>(() => execution.Stop());
        }

        [Test]
        public void ShouldStopStopwatch()
        {
            //Act
            execution.Start();
            execution.Stop();

            var elapsed = execution.Elapsed;

            Thread.Sleep(1);

            //Assert
            Assert.AreEqual(elapsed, execution.Elapsed);
        }

        [Test]
        public void ShouldThrowWhenStoppedMoreThanOnce()
        {
            //Arrange
            Execution execution = new Execution("SectionName");

            //Act & Assert
            execution.Start();
            execution.Stop();

            Assert.Throws<ExecutionIsNotRunningException>(() => execution.Stop());
        }
    }
}