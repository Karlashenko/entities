using Entities.Utility;

namespace Entities.Core;

public struct QueryBuilder
{
    private readonly EntityRegistry registry;

    private Mask256 requiredMask;
    private Mask256 optionalMask;
    private Mask256 excludedMask;

    public QueryBuilder(EntityRegistry registry)
    {
        this.registry = registry;
        this.requiredMask = new Mask256();
        this.optionalMask = new Mask256();
        this.excludedMask = new Mask256();
    }

    public QueryBuilder Require<T>() where T : struct, IComponent
    {
        this.requiredMask = Mask256.Set(this.requiredMask, this.registry.GetComponentTypeId<T>());
        return this;
    }

    public QueryBuilder Option<T>() where T : struct, IComponent
    {
        this.optionalMask = Mask256.Set(this.optionalMask, this.registry.GetComponentTypeId<T>());
        return this;
    }

    public QueryBuilder Exclude<T>() where T : struct, IComponent
    {
        this.excludedMask = Mask256.Set(this.excludedMask, this.registry.GetComponentTypeId<T>());
        return this;
    }

    public Query Build()
    {
        return new Query(this.requiredMask, this.optionalMask, this.excludedMask);
    }
}