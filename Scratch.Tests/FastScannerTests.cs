using System.Text;
using Scratch.Utils;

namespace Scratch.Tests;

[Trait("Scratch", "Unit")]
public sealed class FastScannerTests
{
    [Fact]
    public void NextInt_AsciiWhitespaceAndSignedBoundaries_ReturnsExpectedValues()
    {
        var scanner = ScannerFor("\t\n\v\f\r 0 +42 -17 2147483647 -2147483648");

        Assert.Equal(0, scanner.NextInt());
        Assert.Equal(42, scanner.NextInt());
        Assert.Equal(-17, scanner.NextInt());
        Assert.Equal(int.MaxValue, scanner.NextInt());
        Assert.Equal(int.MinValue, scanner.NextInt());
    }

    [Fact]
    public void NextInt_AfterLastToken_ThrowsEndOfStreamException()
    {
        var scanner = ScannerFor("1");

        Assert.Equal(1, scanner.NextInt());
        Assert.Throws<EndOfStreamException>(() => scanner.NextInt());
    }

    [Theory]
    [InlineData("x")]
    [InlineData("12x")]
    [InlineData("+")]
    [InlineData("-")]
    public void NextInt_InvalidToken_ThrowsFormatException(string input)
    {
        var scanner = ScannerFor(input);

        Assert.Throws<FormatException>(() => scanner.NextInt());
    }

    [Theory]
    [InlineData("2147483648")]
    [InlineData("-2147483649")]
    public void NextInt_OutsideInt32Range_ThrowsOverflowException(string input)
    {
        var scanner = ScannerFor(input);

        Assert.Throws<OverflowException>(() => scanner.NextInt());
    }

    private static FastScanner ScannerFor(string input) =>
        new(new MemoryStream(Encoding.ASCII.GetBytes(input)));
}
