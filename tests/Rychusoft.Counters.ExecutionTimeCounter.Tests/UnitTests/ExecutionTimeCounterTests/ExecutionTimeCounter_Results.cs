using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    [TestFixture]
    public class ExecutionTimeCounter_Results : ExecutionTimeCounterTestsBase
    {
        private int iterations = 5;

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
        public void ShouldReturnAllCodeSections()
        {
            //Assert
            var results = ExecutionTimeCounter.Results();

            Assert.AreEqual(iterations, results.Count);

            foreach (var result in results)
                Assert.AreEqual(1, results.Count(r => r.SectionName == result.SectionName));
        }

        [Test]
        public void ShouldReturnAllExecutions()
        {
            //Assert
            var results = ExecutionTimeCounter.Results();

            foreach (var result in results)
                Assert.AreEqual(iterations * 3, result.Executions.Count);
        }

        [Test]
        public void ShouldComputeAverageCorrectly()
        {
            //Assert
            var results = ExecutionTimeCounter.Results();

            foreach (var result in results)
            {
                Assert.GreaterOrEqual(result.Average, TimeSpan.FromMilliseconds(18));
                Assert.LessOrEqual(result.Average, TimeSpan.FromMilliseconds(22));
            }
        }

        [Test]
        public void ShouldComputeFastestCorrectly()
        {
            //Assert
            var results = ExecutionTimeCounter.Results();

            foreach (var result in results)
            {
                Assert.GreaterOrEqual(result.Fastest, TimeSpan.FromMilliseconds(8));
                Assert.LessOrEqual(result.Fastest, TimeSpan.FromMilliseconds(12));
            }
        }

        [Test]
        public void ShouldComputeSlowestCorrectly()
        {
            //Assert
            var results = ExecutionTimeCounter.Results();

            foreach (var result in results)
            {
                Assert.GreaterOrEqual(result.Slowest, TimeSpan.FromMilliseconds(28));
                Assert.LessOrEqual(result.Slowest, TimeSpan.FromMilliseconds(32));
            }
        }

        [Test]
        public void ShouldComputeMedianCorrectly()
        {
            //Act
            for (int j = 0; j < 32; j++)
            {
                var execution = ExecutionTimeCounter.Start("0");
                Thread.Sleep(1);
                ExecutionTimeCounter.Stop(execution);
            }

            //Assert
            var results = ExecutionTimeCounter.Results();

            Assert.LessOrEqual(results.Single(r => r.SectionName == "0").Median, TimeSpan.FromMilliseconds(3));

            for (int i = 1; i < iterations; i++)
            {
                Assert.GreaterOrEqual(results.Single(r => r.SectionName == i.ToString()).Median, TimeSpan.FromMilliseconds(18));
                Assert.LessOrEqual(results.Single(r => r.SectionName == i.ToString()).Median, TimeSpan.FromMilliseconds(22));
            }
        }
    }
}