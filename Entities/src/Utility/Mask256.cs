using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Entities.Utility;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Mask256
{
    private const int MaskCapacity = sizeof(uint) * 8 - 1;
    private const int MaskShift = 5;

    private readonly uint mask0;
    private readonly uint mask1;
    private readonly uint mask2;
    private readonly uint mask3;
    private readonly uint mask4;
    private readonly uint mask5;
    private readonly uint mask6;
    private readonly uint mask7;

    public Mask256(uint mask0, uint mask1, uint mask2, uint mask3, uint mask4, uint mask5, uint mask6, uint mask7)
    {
        this.mask0 = mask0;
        this.mask1 = mask1;
        this.mask2 = mask2;
        this.mask3 = mask3;
        this.mask4 = mask4;
        this.mask5 = mask5;
        this.mask6 = mask6;
        this.mask7 = mask7;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Mask256 Set(in Mask256 self, in int index)
    {
        var maskIndex = index >> MaskShift;
        var mask = 1u << (index & MaskCapacity);

        var mask0 = self.mask0;
        var mask1 = self.mask1;
        var mask2 = self.mask2;
        var mask3 = self.mask3;
        var mask4 = self.mask4;
        var mask5 = self.mask5;
        var mask6 = self.mask6;
        var mask7 = self.mask7;

        switch (maskIndex)
        {
            case 0:
                mask0 |= mask;
                break;
            case 1:
                mask1 |= mask;
                break;
            case 2:
                mask2 |= mask;
                break;
            case 3:
                mask3 |= mask;
                break;
            case 4:
                mask4 |= mask;
                break;
            case 5:
                mask5 |= mask;
                break;
            case 6:
                mask6 |= mask;
                break;
            case 7:
                mask7 |= mask;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        return new Mask256(mask0, mask1, mask2, mask3, mask4, mask5, mask6, mask7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Mask256 Clear(in Mask256 self, in int index)
    {
        var maskIndex = index >> MaskShift;
        var mask = 1u << (index & MaskCapacity);

        var mask0 = self.mask0;
        var mask1 = self.mask1;
        var mask2 = self.mask2;
        var mask3 = self.mask3;
        var mask4 = self.mask4;
        var mask5 = self.mask5;
        var mask6 = self.mask6;
        var mask7 = self.mask7;

        switch (maskIndex)
        {
            case 0:
                mask0 &= ~mask;
                break;
            case 1:
                mask1 &= ~mask;
                break;
            case 2:
                mask2 &= ~mask;
                break;
            case 3:
                mask3 &= ~mask;
                break;
            case 4:
                mask4 &= ~mask;
                break;
            case 5:
                mask5 &= ~mask;
                break;
            case 6:
                mask6 &= ~mask;
                break;
            case 7:
                mask7 &= ~mask;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        return new Mask256(mask0, mask1, mask2, mask3, mask4, mask5, mask6, mask7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Mask256 Set(int index)
    {
        return Set(this, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Mask256 Clear(int index)
    {
        return Clear(this, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSet(int index)
    {
        var maskIndex = index >> MaskShift;
        var mask = 1 << (index & MaskCapacity);

        switch (maskIndex)
        {
            case 0:
                return (this.mask0 & mask) != 0;
            case 1:
                return (this.mask1 & mask) != 0;
            case 2:
                return (this.mask2 & mask) != 0;
            case 3:
                return (this.mask3 & mask) != 0;
            case 4:
                return (this.mask4 & mask) != 0;
            case 5:
                return (this.mask5 & mask) != 0;
            case 6:
                return (this.mask6 & mask) != 0;
            case 7:
                return (this.mask7 & mask) != 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
    {
        return this.mask0 == 0 &&
               this.mask1 == 0 &&
               this.mask2 == 0 &&
               this.mask3 == 0 &&
               this.mask4 == 0 &&
               this.mask5 == 0 &&
               this.mask6 == 0 &&
               this.mask7 == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsAll(in Mask256 other)
    {
        return (this.mask0 & other.mask0) == other.mask0 &&
               (this.mask1 & other.mask1) == other.mask1 &&
               (this.mask2 & other.mask2) == other.mask2 &&
               (this.mask3 & other.mask3) == other.mask3 &&
               (this.mask4 & other.mask4) == other.mask4 &&
               (this.mask5 & other.mask5) == other.mask5 &&
               (this.mask6 & other.mask6) == other.mask6 &&
               (this.mask7 & other.mask7) == other.mask7;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsAny(in Mask256 other)
    {
        return (other.mask0 == 0 || (this.mask0 & other.mask0) > 0) &&
               (other.mask1 == 0 || (this.mask1 & other.mask1) > 0) &&
               (other.mask2 == 0 || (this.mask2 & other.mask2) > 0) &&
               (other.mask3 == 0 || (this.mask3 & other.mask3) > 0) &&
               (other.mask4 == 0 || (this.mask4 & other.mask4) > 0) &&
               (other.mask5 == 0 || (this.mask5 & other.mask5) > 0) &&
               (other.mask6 == 0 || (this.mask6 & other.mask6) > 0) &&
               (other.mask7 == 0 || (this.mask7 & other.mask7) > 0);
    }
}