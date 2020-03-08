using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rychusoft.Counters.ExecutionTime.Tests.UnitTests.ExecutionTimeCounterTests
{
    [TestFixture]
    public class ExecutionTimeCounter_Results : ExecutionTimeCounterTestsBase
    {
        private readonly int iterations = 5;

        [SetUp]
        public void SetUp()
        {
            CreateExecutions(iterations, 10);
            CreateExecutions(iterations, 20);
            CreateExecutions(iterations, 30);
        }

        [Test]
        public void ShouldReturnAllCodeSections()
        {
            //Act
            var results = ExecutionTimeCounter.Results();

            //Assert
            Assert.AreEqual(iterations, results.Count);

            foreach (var result in results)
                Assert.AreEqual(1, results.Count(r => r.SectionName == result.SectionName));
        }

        [Test]
        public void ShouldReturnAllExecutions()
        {
            //Act
            var results = ExecutionTimeCounter.Results();

            //Assert
            foreach (var result in results)
                Assert.AreEqual(iterations * 3, result.Executions.Count);
        }

        [Test]
        public void ShouldComputeAverageCorrectly()
        {
            //Act
            var results = ExecutionTimeCounter.Results();

            //Assert
            foreach (var result in results)
            {
                double averageExpected = executionsDictionary[result.SectionName].Average();
                Assert.AreEqual(averageExpected, result.Average.TotalMilliseconds, 0.001);
            }
        }

        [Test]
        public void ShouldComputeFastestCorrectly()
        {
            //Act
            var results = ExecutionTimeCounter.Results();

            //Assert
            foreach (var result in results)
            {
                double fastestExpected = executionsDictionary[result.SectionName].Min();
                Assert.AreEqual(fastestExpected, result.Fastest.TotalMilliseconds, 0.001);
            }
        }

        [Test]
        public void ShouldComputeSlowestCorrectly()
        {
            //Act
            var results = ExecutionTimeCounter.Results();

            //Assert
            foreach (var result in results)
            {
                double slowestExpected = executionsDictionary[result.SectionName].Max();
                Assert.AreEqual(slowestExpected, result.Slowest.TotalMilliseconds, 0.001);
            }
        }

        [Test]
        public void ForOddExecutionsCount_ShouldComputeMedianCorrectly()
        {
            //Act
            for (int j = 0; j < 32; j++)
            {
                var execution = ExecutionTimeCounter.Start("0");
                Thread.Sleep(1);
                ExecutionTimeCounter.Stop(execution);

                executionsDictionary["0"].Add(execution.Elapsed.TotalMilliseconds);
            }

            var results = ExecutionTimeCounter.Results();

            //Assert
            var medianExpected = executionsDictionary["0"].OrderBy(x => x).ElementAt(23);
            Assert.AreEqual(medianExpected, results.Single(r => r.SectionName == "0").Median.TotalMilliseconds, 0.001);

            for (int i = 1; i < iterations; i++)
            {
                medianExpected = executionsDictionary[i.ToString()].OrderBy(x => x).ElementAt(7);
                Assert.AreEqual(medianExpected, results.Single(r => r.SectionName == i.ToString()).Median.TotalMilliseconds, 0.001);
            }
        }

        [Test]
        public void ForEvenUnsortedExecutionsCount_ShouldComputeMedianCorrectly()
        {
            //Act
            List<double> executions = new List<double>();

            var execution = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(5);
            ExecutionTimeCounter.Stop(execution);

            executions.Add(execution.Elapsed.TotalMilliseconds);

            execution = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(1);
            ExecutionTimeCounter.Stop(execution);

            executions.Add(execution.Elapsed.TotalMilliseconds);

            execution = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(3);
            ExecutionTimeCounter.Stop(execution);

            executions.Add(execution.Elapsed.TotalMilliseconds);

            execution = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(7);
            ExecutionTimeCounter.Stop(execution);

            executions.Add(execution.Elapsed.TotalMilliseconds);

            var orderedExecutions = executions.OrderBy(x => x);

            var medianExpected = (orderedExecutions.ElementAt(1) + orderedExecutions.ElementAt(2)) / 2.0;
            var medianActual = ExecutionTimeCounter.Results().Single(r => r.SectionName == "TEST").Median;

            //Assert
            Assert.AreEqual(medianExpected, medianActual.TotalMilliseconds, 0.001);
        }

        [Test]
        public void ForOneExecution_ShouldComputeMedianCorrectly()
        {
            //Act
            var execution = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(4);
            ExecutionTimeCounter.Stop(execution);

            var median = ExecutionTimeCounter.Results().Single(r => r.SectionName == "TEST").Median;

            //Assert
            Assert.AreEqual(execution.Elapsed, median);
        }

        [Test]
        public void ForTwoExecutions_ShouldComputeMedianCorrectly()
        {
            //Act
            var execution1 = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(3);
            ExecutionTimeCounter.Stop(execution1);

            var execution2 = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(5);
            ExecutionTimeCounter.Stop(execution2);

            var medianExpected = (execution1.Elapsed.TotalMilliseconds + execution2.Elapsed.TotalMilliseconds) / 2.0;
            var median = ExecutionTimeCounter.Results().Single(r => r.SectionName == "TEST").Median;

            //Assert
            Assert.AreEqual(medianExpected, median.TotalMilliseconds, 0.001);
        }

        [Test]
        public void ForThreeExecutions_ShouldComputeMedianCorrectly()
        {
            //Act
            List<double> executions = new List<double>();

            var execution1 = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(1);
            ExecutionTimeCounter.Stop(execution1);

            executions.Add(execution1.Elapsed.TotalMilliseconds);

            var execution2 = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(6);
            ExecutionTimeCounter.Stop(execution2);

            executions.Add(execution2.Elapsed.TotalMilliseconds);

            var execution3 = ExecutionTimeCounter.Start("TEST");
            Thread.Sleep(1);
            ExecutionTimeCounter.Stop(execution3);

            executions.Add(execution3.Elapsed.TotalMilliseconds);

            var medianExpected = executions.OrderBy(x => x).ElementAt(1);
            var median = ExecutionTimeCounter.Results().Single(r => r.SectionName == "TEST").Median;

            //Assert
            Assert.AreEqual(medianExpected, median.TotalMilliseconds, 0.001);
        }
    }
}