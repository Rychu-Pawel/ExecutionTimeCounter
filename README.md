# ExecutionTimeCounter
Library for measuring execution times of code sections

```csharp
foreach (var item in items)
{
    Prepare();
	
    var execution1 = ExecutionTimeCounter.Start("Meaningful 1");
    DoMeaningful();
    ExecutionTimeCounter.Stop(execution1);
	
	var execution2 = ExecutionTimeCounter.Start("Meaningful 2");
    DoMeaningful();
    ExecutionTimeCounter.Stop(execution2);
	
	Cleanup();
}

var result = ExecutionTimeCounter.ResultsToString();
Console.WriteLine(result);
```