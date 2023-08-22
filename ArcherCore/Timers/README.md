## ArcherCore.Timers

A set of timer utilities.

### Example and explanation

Function examples below:

```csharp
using ArcherCore.Timers;

// Creates and starts a new timer.
var timer = Timer.Initiate();

var seconds = timer.GetElapsedSeconds();
var milliseconds = timer.GetElapsedMilliseconds();
var now = timer.Now();

timer.Stop();
timer.Reset();
timer.Start();
timer.Restart();
```
