using Scratch.Utils;
using Scratch.leetcode;
using Scratch.leetcode.DataStructure;

namespace Scratch.Tests;

public class UnitTests
{
    [Fact]
    public void AbsTest()
    {
        Assert.Equal(4, Math.Abs(-4));
    }

    [Fact]
    public void AbsFailTest()
    {
        // Given

        // When

        // Then
        Assert.NotEqual(4, Math.Abs(-5));
    }

    /// <summary>
    /// tests of leetcode problems
    /// </summary>
}
