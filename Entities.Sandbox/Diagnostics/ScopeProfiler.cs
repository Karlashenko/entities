using System.Diagnostics;

namespace Entities.Sandbox.Diagnostics;

public class ScopeProfiler : IDisposable
{
    private readonly Stopwatch stopwatch;
    private readonly string label;

    public ScopeProfiler(string label)
    {
        this.label = label;

        this.stopwatch = new Stopwatch();
        this.stopwatch.Start();
    }

    public void Dispose()
    {
        Console.WriteLine($"{this.label}: {this.stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}