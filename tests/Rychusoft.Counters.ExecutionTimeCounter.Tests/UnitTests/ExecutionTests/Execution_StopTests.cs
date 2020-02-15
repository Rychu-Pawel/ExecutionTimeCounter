using NUnit.Framework;
using Rychusoft.Counters.ExecutionTime;

namespace Rychusoft.Counters.ExecutionTimeCounter.Tests.UnitTests.ExecutionTests
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
        public void ShouldNotThrowWhenNotStarted()
        {
            //Act & Assert
            Assert.DoesNotThrow(() => execution.Stop());
        }
    }
}