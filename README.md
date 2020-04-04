# Status
Tool  | Status
-------- | :------------:
SonarCloud | [![reliability](https://sonarcloud.io/api/project_badges/measure?project=Rychu-Pawel_ExecutionTimeCounter&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=Rychu-Pawel_ExecutionTimeCounter) [![security](https://sonarcloud.io/api/project_badges/measure?project=Rychu-Pawel_ExecutionTimeCounter&metric=security_rating)](https://sonarcloud.io/dashboard?id=Rychu-Pawel_ExecutionTimeCounter) [![vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Rychu-Pawel_ExecutionTimeCounter&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=Rychu-Pawel_ExecutionTimeCounter)
Nuget | [![Nuget](https://img.shields.io/nuget/v/Rychusoft.Counters.ExecutionTimeCounter.svg?style=flat)](https://www.nuget.org/packages/Rychusoft.Counters.ExecutionTimeCounter/)
Code coverage | [![coverage](https://sonarcloud.io/api/project_badges/measure?project=Rychu-Pawel_ExecutionTimeCounter&metric=coverage)](https://sonarcloud.io/dashboard?id=Rychu-Pawel_ExecutionTimeCounter)

# ExecutionTimeCounter
Library for measuring execution times of code sections

```csharp
foreach (var item in items)
{
    var prepareExecution = ExecutionTimeCounter.Start("Prepare");
    Prepare(item);
    ExecutionTimeCounter.Stop(prepareExecution);

    var execution1 = ExecutionTimeCounter.Start("Meaningful 1");
    DoMeaningful1(item);
    ExecutionTimeCounter.Stop(execution1);

    var execution2 = ExecutionTimeCounter.Start("Meaningful 2");
    DoMeaningful2(item);
    ExecutionTimeCounter.Stop(execution2);

    var cleanupExecution = ExecutionTimeCounter.Start("Cleanup");
    Cleanup(item);
    ExecutionTimeCounter.Stop(cleanupExecution);
}

var result = ExecutionTimeCounter.ResultsToString();
Console.WriteLine(result);
```
Output:
```
Cleanup:
  Average: 00:00:00.1505103
  Median: 00:00:00.1503439
  Fastest: 00:00:00.1501001
  Slowest: 00:00:00.1512535
  Executions: 4

Meaningful 1:
  Average: 00:00:05.0005940
  Median: 00:00:05.0003949
  Fastest: 00:00:05.0001948
  Slowest: 00:00:05.0013913
  Executions: 4

Meaningful 2:
  Average: 00:00:01.2006227
  Median: 00:00:01.2005889
  Fastest: 00:00:01.2003572
  Slowest: 00:00:01.2009559
  Executions: 4

Prepare:
  Average: 00:00:00.2806096
  Median: 00:00:00.2804891
  Fastest: 00:00:00.2801096
  Slowest: 00:00:00.2811026
  Executions: 4
```
