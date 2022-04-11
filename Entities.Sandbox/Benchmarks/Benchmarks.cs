using BenchmarkDotNet.Attributes;

namespace Entities.Sandbox.Benchmarks;

public class Benchmarks
{
    private const int OperationsPerInvoke = 64;
    private const int NumberOfEntities = 1_000_000;

    private readonly RegularApproachBenchmarks regularApproachBenchmarks;
    private readonly EntityComponentSystemBenchmarks entityComponentSystemBenchmarks;

    public Benchmarks()
    {
        this.regularApproachBenchmarks = new RegularApproachBenchmarks(NumberOfEntities);
        this.entityComponentSystemBenchmarks = new EntityComponentSystemBenchmarks(NumberOfEntities);
    }

    [Benchmark(OperationsPerInvoke = OperationsPerInvoke)]
    public void RegularApproach()
    {
        this.regularApproachBenchmarks.Iterate();
    }

    [Benchmark(OperationsPerInvoke = OperationsPerInvoke)]
    public void EntityComponentSystem()
    {
        this.entityComponentSystemBenchmarks.Iterate();
    }
}