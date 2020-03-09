using NUnit.Framework;
using System;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTests
{
    [TestFixture]
    public class Execution_ContructorTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ShouldThrowOnNullOrEmptyParameter(string sectionName)
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Execution(sectionName));
        }

        [TestCase("SectionName")]
        public void ShouldNotThrowOnCorrectParameter(string sectionName)
        {
            //Act & Assert
            Assert.DoesNotThrow(() => new Execution(sectionName));
        }

        [TestCase("SectionName")]
        [TestCase("SectionName2")]
        [TestCase("qwerty")]
        public void ShouldSetSectionNameCorrectly(string sectionName)
        {
            //Arrange & Act
            var execution = new Execution(sectionName);

            //Assert
            Assert.AreEqual(sectionName, execution.SectionName);
        }

        [Test]
        public void ShouldInitializeStopwatchWithoutStartingIt()
        {
            //Arrange & Act
            var execution = new Execution("SectionName");

            //Assert
            Assert.AreEqual(new TimeSpan(0), execution.Elapsed);
        }

        [Test]
        public void ShouldInitializeStartedDateTimeToZero()
        {
            //Arrange & Act
            var execution = new Execution("SectionName");

            //Assert
            Assert.AreEqual(new DateTime(0, DateTimeKind.Local), execution.Started);
        }
    }
}