using System.Linq;
using Entities.Extensions;
using Xunit;

namespace Entities.Tests.Extensions;

public class CollectionExtensionsTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(9)]
    public void IndexInBounds_ReturnsTrue_WhenIndexInBounds(int index)
    {
        var collection = Enumerable.Range(0, 10).ToList();

        Assert.True(collection.IndexInBounds(index));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10)]
    [InlineData(99)]
    public void IndexInBounds_ReturnsFalse_WhenIndexOutOfBounds(int index)
    {
        var collection = Enumerable.Range(0, 10).ToList();

        Assert.False(collection.IndexInBounds(index));
    }
}