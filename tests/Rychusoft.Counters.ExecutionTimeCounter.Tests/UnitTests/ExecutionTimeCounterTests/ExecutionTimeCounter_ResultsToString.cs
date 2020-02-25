using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    [TestFixture]
    public class ExecutionTimeCounter_ResultsToString : ExecutionTimeCounterTestsBase
    {
        private readonly int iterations = 5;

        [SetUp]
        public void SetUp()
        {
            CreateExecutions(iterations, 10);
            CreateExecutions(iterations, 20);
            CreateExecutions(iterations, 30);
        }

        [TearDown]
        public void TearDown()
        {
            ExecutionTimeCounter.Reset();
        }

        [Test]
        public void ShouldContainAllSectionNames()
        {
            //Act
            var result = ExecutionTimeCounter.ResultsToString();

            //Assert
            for (int i = 0; i < 5; i++)
                Assert.IsTrue(result.Contains($"{i}:"));
        }

        [Test]
        public void ShouldContainAllStats()
        {
            //Act
            var result = ExecutionTimeCounter.ResultsToString();

            //Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(iterations, new Regex("Average").Matches(result).Count);
                Assert.AreEqual(iterations, new Regex("Median").Matches(result).Count);
                Assert.AreEqual(iterations, new Regex("Fastest").Matches(result).Count);
                Assert.AreEqual(iterations, new Regex("Slowest").Matches(result).Count);
                Assert.AreEqual(iterations, new Regex("Executions").Matches(result).Count);
            }
        }

        [Test]
        public void ShouldContainExpectedNumberOfLines()
        {
            //Act
            var result = ExecutionTimeCounter.ResultsToString();

            //Assert
            int expectedLinesPerSection = 7;
            Assert.AreEqual(iterations * expectedLinesPerSection, new Regex(Environment.NewLine).Matches(result).Count);
        }
    }
}