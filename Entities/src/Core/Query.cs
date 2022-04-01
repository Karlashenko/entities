using System.Runtime.InteropServices;
using Entities.Utility;

namespace Entities.Core;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Query
{
    public readonly Mask256 RequiredMask;
    public readonly Mask256 OptionalMask;
    public readonly Mask256 ExcludedMask;

    public Query(Mask256 requiredMask, Mask256 optionalMask, Mask256 excludedMask)
    {
        this.RequiredMask = requiredMask;
        this.OptionalMask = optionalMask;
        this.ExcludedMask = excludedMask;
    }
}