using System;
using Entities.Utility;
using Xunit;

namespace Entities.Tests.Utility;

public class Mask256Tests
{
    [Fact]
    public void Set_ThrowsException_WhenIndexIsOutOfMaskRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var mask = new Mask256();
            mask.Set(256);
        });
    }

    [Fact]
    public void IsSet_ThrowsException_WhenIndexIsOutOfMaskRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var mask = new Mask256();
            mask.IsSet(256);
        });
    }

    [Fact]
    public void Clear_ThrowsException_WhenIndexIsOutOfMaskRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var mask = new Mask256();
            mask.Clear(256);
        });
    }

    [Fact]
    public void IsEmpty_ReturnsTrue_WhenEmpty()
    {
        var mask = new Mask256();

        Assert.True(mask.IsEmpty());
    }

    [Fact]
    public void IsEmpty_ReturnsFalse_WhenNotEmpty()
    {
        var mask = new Mask256().Set(32);

        Assert.False(mask.IsEmpty());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(31)]
    [InlineData(63)]
    [InlineData(95)]
    [InlineData(127)]
    [InlineData(159)]
    [InlineData(191)]
    [InlineData(223)]
    [InlineData(255)]
    public void IsSet_ReturnsTrue_WhenCorrespondingFlagIsSet(int index)
    {
        var mask = new Mask256().Set(index);

        Assert.True(mask.IsSet(index));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(31)]
    [InlineData(63)]
    [InlineData(95)]
    [InlineData(127)]
    [InlineData(159)]
    [InlineData(191)]
    [InlineData(223)]
    [InlineData(255)]
    public void IsSet_ReturnsFalse_WhenCorrespondingFlagIsNotSet(int index)
    {
        var mask = new Mask256().Set(index);

        Assert.False(mask.IsSet(index - 1));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(31)]
    [InlineData(63)]
    [InlineData(95)]
    [InlineData(127)]
    [InlineData(159)]
    [InlineData(191)]
    [InlineData(223)]
    [InlineData(255)]
    public void Clear_CorrespondingFlagIsNotSet_AfterClear(int index)
    {
        var mask = new Mask256().Set(index);

        mask = mask.Clear(index);

        Assert.False(mask.IsSet(index));
    }

    [Theory]
    [InlineData(1 << 1)]
    [InlineData(1 << 2)]
    [InlineData(1 << 3)]
    [InlineData(1 << 4)]
    [InlineData(1 << 5)]
    [InlineData(1 << 6)]
    [InlineData(1 << 7)]
    public void ContainsAll_ReturnsTrue_WhenContainsAllFlags(int flags)
    {
        var mask = new Mask256().Set(flags);

        Assert.True(mask.ContainsAll(mask));
    }

    [Theory]
    [InlineData(1 << 1)]
    [InlineData(1 << 2)]
    [InlineData(1 << 3)]
    [InlineData(1 << 4)]
    [InlineData(1 << 5)]
    [InlineData(1 << 6)]
    [InlineData(1 << 7)]
    public void ContainsAny_ReturnsTrue_WhenContainsAnyFlags(int flags)
    {
        var mask = new Mask256().Set(flags);

        Assert.True(mask.ContainsAny(mask));
    }
}