# Entities

The simplest implementation of the Entity Component System.

    macOS Monterey 12.2.1 (21D62) [Darwin 21.3.0]
    Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
    
    [Host]     : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT
    DefaultJob : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT

| Method                |        Mean |     Error |    StdDev |
|-----------------------|------------:|----------:|----------:|
| RegularApproach       | 1,053.49 us | 12.968 us | 12.130 us |
| EntityComponentSystem |    27.74 us |  0.045 us |  0.038 us |