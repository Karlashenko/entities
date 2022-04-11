using System.Numerics;

namespace Entities.Sandbox.Benchmarks;

public class RegularApproachBenchmarks
{
    private readonly Entity[] entities;

    public RegularApproachBenchmarks(int count = 100000)
    {
        this.entities = new Entity[count];

        for (var i = 0; i < count; i++)
        {
            var entity = new Entity();

            entity.AddComponent(new PositionComponent());

            if (i % 5 == 0)
            {
                entity.AddComponent(new VelocityComponent());
            }

            this.entities[i] = entity;
        }
    }

    public void Iterate()
    {
        foreach (var entity in this.entities)
        {
            var positionComponent = entity.GetComponent<PositionComponent>();
            var velocityComponent = entity.GetComponent<VelocityComponent>();

            if (positionComponent == null || velocityComponent == null)
            {
                continue;
            }

            positionComponent.Vector += velocityComponent.Vector;
        }
    }

    private class Entity
    {
        private readonly Dictionary<Type, object> components = new();

        public void AddComponent<T>(T component) where T : class
        {
            this.components.Add(typeof(T), component);
        }

        public T? GetComponent<T>() where T : class
        {
            return this.components.GetValueOrDefault(typeof(T)) as T;
        }
    }

    private class PositionComponent
    {
        public Vector3 Vector = Vector3.Zero;
    }

    private class VelocityComponent
    {
        public Vector3 Vector = Vector3.One;
    }
}