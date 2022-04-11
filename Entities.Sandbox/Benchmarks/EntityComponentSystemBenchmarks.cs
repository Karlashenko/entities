using System.Numerics;
using Entities.Core;
using Entities.Sandbox.Components;

namespace Entities.Sandbox.Benchmarks;

public class EntityComponentSystemBenchmarks
{
    private readonly EntityRegistry registry;
    private readonly Query query;

    public EntityComponentSystemBenchmarks(int count = 100000)
    {
        this.registry = new EntityRegistry();

        for (var i = 0; i < count; i++)
        {
            var entityId = this.registry.Create();
            this.registry.AddComponent(entityId, new PositionComponent());

            if (i % 5 == 0)
            {
                this.registry.AddComponent(entityId, new VelocityComponent {Vector = new Vector3(1, 0, 0)});
            }
        }

        this.query = new QueryBuilder(this.registry)
            .Require<VelocityComponent>()
            .Require<PositionComponent>()
            .Build();
    }

    public void Iterate()
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
}