namespace Scratch.Tests;

[Collection("Console")]
public class AcmTests
{
    [Theory]
    [InlineData("5\n1 2 3 4 5\n", "15")]
    [InlineData("\t3\r\n-5  2\t10\n", "7")]
    [InlineData("2\n3000000000 4000000000\n", "7000000000")]
    [InlineData("0\n", "0")]
    public void Acm_ReadsCountAndIntegers_WritesSum(string input, string expected)
    {
        AssertOutput(Labuladong.Solution.Acm, input, expected + Environment.NewLine);
    }

    [Theory]
    [InlineData("2\n1 2 3\n-5\t10\n", "6\n5\n")]
    [InlineData("1\r\n3000000000 4000000000", "7000000000\n")]
    [InlineData("0\n", "")]
    public void AcmMultipleCases_ReadsSpecifiedGroups(string input, string expected)
    {
        AssertOutput(Labuladong.Solution.AcmMultipleCases, input, expected.Replace("\n", Environment.NewLine));
    }

    [Theory]
    [InlineData("1 2 3\n-5\t10", "6\n5\n")]
    [InlineData("3000000000 4000000000\r\n", "7000000000\n")]
    [InlineData("", "")]
    public void AcmUntilEof_ReadsAllLines(string input, string expected)
    {
        AssertOutput(Labuladong.Solution.AcmUntilEof, input, expected.Replace("\n", Environment.NewLine));
    }

    [Theory]
    [InlineData("  hello world  \nignored", "  hello world  \n")]
    [InlineData("\n", "\n")]
    [InlineData("", "")]
    public void AcmText_PreservesSpacesAndDistinguishesEof(string input, string expected)
    {
        AssertOutput(Labuladong.Solution.AcmText, input, expected.Replace("\n", Environment.NewLine));
    }

    private static void AssertOutput(Action run, string input, string expected)
    {
        var originalInput = Console.In;
        var originalOutput = Console.Out;
        using var reader = new StringReader(input);
        using var writer = new StringWriter();
        try
        {
            Console.SetIn(reader);
            Console.SetOut(writer);

            run();

            Assert.Equal(expected, writer.ToString());
        }
        finally
        {
            Console.SetIn(originalInput);
            Console.SetOut(originalOutput);
        }
    }
}
