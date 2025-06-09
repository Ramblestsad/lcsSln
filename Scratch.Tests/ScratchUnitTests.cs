namespace Scratch.Tests;
[Trait("Scratch", "Unit")]
public class ScratchUnitTests : IDisposable
{
    public ScratchUnitTests()
    {
        throw new NotImplementedException();
    }

    [Fact]
    [Trait("Scratch", "Dummy")]
    public void AbsTest()
    {
        Assert.Equal(4, Math.Abs(-4));
    }

    [Fact]
    [Trait("Scratch", "Dummy")]
    public void AbsFailTest()
    {
        Assert.NotEqual(4, Math.Abs(-5));
    }

    [Theory]
    [Trait("Scratch", "Dummy")]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(7)]
    public void IsEvenTheory(int number)
    {
        Assert.True(IsEven(number));
    }

    bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public void Dispose()
    {
        // Do some clean-up for every test
        throw new NotImplementedException();
    }
}
