using Entities.Utility;

namespace Entities.Core;

public readonly struct Entity
{
    public readonly int Index;
    public readonly string Name;
    public readonly Mask256 Mask;

    public Entity(int index, string name)
    {
        this.Index = index;
        this.Name = name;
        this.Mask = new Mask256();
    }

    public Entity(int index, string name, Mask256 mask)
    {
        this.Index = index;
        this.Name = name;
        this.Mask = mask;
    }

    public Entity WithMask(Mask256 mask)
    {
        return new Entity(this.Index, this.Name, mask);
    }

    public override string ToString()
    {
        var label = string.IsNullOrEmpty(this.Name) ? "Entity" : this.Name;
        return $"{label} {this.Index.ToString()}";
    }
}