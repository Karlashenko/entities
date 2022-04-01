using System.Collections;
using Entities.Extensions;
using Entities.Utility;

namespace Entities.Core
{
    public class EntityRegistry
    {
        private const int MaxComponentTypes = 256;
        private const int DefaultEntityArrayCapacity = 512;

        private readonly Queue<int> entityGraveyard = new(32);
        private Entity[] entities = new Entity[DefaultEntityArrayCapacity];
        private int[] entityIndexBuffer = new int[DefaultEntityArrayCapacity];
        private int entityIdCounter = -1;

        private readonly Dictionary<Type, int> componentTypeIds = new(MaxComponentTypes);
        private readonly IList[] entityComponents = new IList[MaxComponentTypes];

        public ReadOnlySpan<int> Query(Query query)
        {
            var matchQueryRequired = !query.RequiredMask.IsEmpty();
            var matchQueryOptional = !query.OptionalMask.IsEmpty();
            var matchQueryExcluded = !query.ExcludedMask.IsEmpty();
            var matchedEntitiesCount = 0;

            for (var entityId = 0; entityId <= this.entityIdCounter; entityId++)
            {
                ref readonly var entity = ref this.entities[entityId];

                if (entity.Mask.IsEmpty())
                {
                    continue;
                }

                if (matchQueryRequired && !entity.Mask.ContainsAll(query.RequiredMask) ||
                    matchQueryOptional && !entity.Mask.ContainsAny(query.OptionalMask) ||
                    matchQueryExcluded && query.ExcludedMask.ContainsAny(entity.Mask))
                {
                    continue;
                }

                this.entityIndexBuffer[matchedEntitiesCount++] = entity.Index;
            }

            return new ReadOnlySpan<int>(this.entityIndexBuffer, 0, matchedEntitiesCount);
        }

        public int Create(string name = "Entity")
        {
            var entityId = this.entityGraveyard.Count > 0
                ? this.entityGraveyard.Dequeue()
                : Interlocked.Increment(ref this.entityIdCounter);

            if (!this.entities.IndexInBounds(entityId))
            {
                Array.Resize(ref this.entities, Math.Max(entityId, 1) * 2);
                Array.Resize(ref this.entityIndexBuffer, Math.Max(entityId, 1) * 2);
            }

            this.entities[entityId] = new Entity(entityId, name);

            return entityId;
        }

        public void Destroy(int entityId)
        {
            TriggerComponentOnDestroy(entityId);

            this.entityGraveyard.Enqueue(entityId);
            this.entities[entityId] = new Entity();
        }

        public bool HasComponent<T>(int entityId) where T : struct, IComponent
        {
            return this.entities[entityId].Mask.IsSet(GetComponentTypeId<T>());
        }

        public ref T GetComponent<T>(int entityId) where T : struct, IComponent
        {
            return ref GetComponents<T>()[entityId];
        }

        public T[] GetComponents<T>() where T : struct, IComponent
        {
            var componentTypeId = GetComponentTypeId<T>();

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (this.entityComponents[componentTypeId] == null)
            {
                this.entityComponents[componentTypeId] = new T[Math.Max(this.entityIdCounter * 2, DefaultEntityArrayCapacity)];
            }

            return (T[]) this.entityComponents[componentTypeId];
        }

        public void AddComponent<T>(int entityId, T component) where T : struct, IComponent
        {
            var componentTypeId = GetComponentTypeId<T>();

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (this.entityComponents[componentTypeId] == null)
            {
                this.entityComponents[componentTypeId] = new T[Math.Max(this.entityIdCounter * 2, DefaultEntityArrayCapacity)];
            }

            var components = (T[]) this.entityComponents[componentTypeId];

            if (!components.IndexInBounds(entityId))
            {
                Array.Resize(ref components, Math.Max(entityId, 1) * 2);
            }

            components[entityId] = component;
            this.entityComponents[componentTypeId] = components;

            ref var entity = ref this.entities[entityId];
            entity = entity.WithMask(Mask256.Set(entity.Mask, componentTypeId));

            component.OnAdd();
        }

        public void RemoveComponent<T>(int entityId) where T : struct, IComponent
        {
            var componentTypeId = GetComponentTypeId<T>();

            ref var entity = ref this.entities[entityId];
            entity = entity.WithMask(Mask256.Clear(entity.Mask, componentTypeId));

            if (this.entityComponents[componentTypeId][entityId] is IComponent component)
            {
                component.OnRemove();
            }
        }

        public int GetComponentTypeId<T>() where T : struct, IComponent
        {
            return GetComponentTypeId(typeof(T));
        }

        public int GetComponentTypeId(Type type)
        {
            if (!this.componentTypeIds.ContainsKey(type))
            {
                this.componentTypeIds[type] = this.componentTypeIds.Count;
            }

            return this.componentTypeIds[type];
        }

        private void TriggerComponentOnDestroy(int entityId)
        {
            foreach (var componentTypeId in this.componentTypeIds.Values)
            {
                if (!this.entityComponents[componentTypeId].IndexInBounds(entityId))
                {
                    continue;
                }

                if (this.entityComponents[componentTypeId][entityId] is IComponent component)
                {
                    component.OnDestroy();
                }
            }
        }
    }
}