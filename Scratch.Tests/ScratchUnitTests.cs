using FluentAssertions;

namespace Scratch.Tests;

[Trait("Scratch", "Unit")]
public class ScratchUnitTests: IDisposable
{
    public void Dispose()
    {
        // Do some cleanup for every test
    }

    [Fact]
    [Trait("Scratch", "Dummy")]
    public void AbsTest()
    {
        Math.Abs(-4).Should().Be(4);
    }

    [Fact]
    [Trait("Scratch", "Dummy")]
    public void AbsFailTest()
    {
        Assert.NotEqual(4, Math.Abs(-5));
    }

    [Theory]
    [Trait("Scratch", "Dummy")]
    [InlineData(2, true)]
    [InlineData(4, true)]
    [InlineData(7, false)]
    public void IsEvenTheory(int number, bool expected)
    {
        IsEven(number).Should().Be(expected);
    }

    bool IsEven(int number)
    {
        return number % 2 == 0;
    }
}
