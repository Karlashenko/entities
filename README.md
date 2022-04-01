# Entities

The simplest implementation of the Entity Component System.

    public EntityRegistryBenchmarks(int count = 100000)
    {
        this.registry = new EntityRegistry();

        for (var i = 0; i < count; i++)
        {
            var entityId = this.registry.Create();
            this.registry.AddComponent(entityId, new VelocityComponent {Vector = new Vector3(1, 0, 0)});
            this.registry.AddComponent(entityId, new PositionComponent());
        }

        this.query = new QueryBuilder(this.registry)
            .Require<VelocityComponent>()
            .Require<PositionComponent>()
            .Build();
    }

    [Benchmark]
    public void IterateA()
    {
        var velocities = this.registry.GetComponents<VelocityComponent>();
        var positions = this.registry.GetComponents<PositionComponent>();

        foreach (var entityId in this.registry.Query(this.query))
        {
            var velocity = velocities[entityId];
            ref var position = ref positions[entityId];

            position.Vector += velocity.Vector;
        }
    }

    [Benchmark]
    public void IterateB()
    {
        foreach (var entityId in this.registry.Query(this.query))
        {
            var velocity = this.registry.GetComponent<VelocityComponent>(entityId);
            ref var position = ref this.registry.GetComponent<PositionComponent>(entityId);

            position.Vector += velocity.Vector;
        }
    }

|   Method |       Mean |    Error |   StdDev | Allocated |
|--------- |-----------:|---------:|---------:|----------:|
| IterateA |   462.1 us |  1.19 us |  1.06 us |         - |
| IterateB | 8,303.5 us | 24.73 us | 23.13 us |      11 B |

