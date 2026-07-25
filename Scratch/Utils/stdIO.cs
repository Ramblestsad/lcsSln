namespace Scratch.Utils;

public static class StdRead
{
    /// <summary>
    /// 读取一行字符串
    /// </summary>
    /// <returns>string</returns>
    public static string? ReadOneLine()
    {
        var line = Console.ReadLine();

        return line;
    }

    /// <summary>
    /// 按空格读取多个值
    /// </summary>
    /// <returns>(int, long)</returns>
    public static (int, long) ReadMultiVal()
    {
        var parts = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var n = int.Parse(parts?[0] ?? string.Empty);
        var m = long.Parse(parts?[1] ?? string.Empty);

        return ( n, m );
    }

    /// <summary>
    /// 读取一行并解析成数组
    /// </summary>
    /// <returns>int[]</returns>
    public static int[] ReadToArr()
    {
        var nums = Console.ReadLine()?
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? [];

        return nums;
    }

    /// <summary>
    /// 连续读取多行输入（直到 EOF）
    /// </summary>
    public static List<string> ReadMultiLines()
    {
        var lines = new List<string>();

        string? line;
        while (( line = Console.ReadLine() ) != null)
        {
            lines.Add(line);
        }

        return lines;
    }

    public static List<string> ReadMultiLinesWithN()
    {
        var n = int.Parse(Console.ReadLine()!);
        var lines = new List<string>();

        for (var i = 0; i < n; i++)
        {
            var line = Console.ReadLine()!;
            lines.Add(line);
        }

        return lines;
    }
}

public sealed class FastScanner
{
    private readonly Stream _stream;
    private readonly byte[] _buffer = new byte[1 << 16];
    private int _len, _ptr;

    public FastScanner(Stream? stream = null)
    {
        _stream = stream ?? Console.OpenStandardInput();
    }

    private int ReadByte()
    {
        if (_ptr >= _len)
        {
            _len = _stream.Read(_buffer, 0, _buffer.Length);
            _ptr = 0;
            if (_len == 0) return -1;
        }

        return _buffer[_ptr++];
    }

    private int NextNonWhitespace()
    {
        int c;
        while (( c = ReadByte() ) <= ' ')
        {
            if (c == -1)
                throw new EndOfStreamException();
        }

        return c;
    }

    public int NextInt()
    {
        var c = NextNonWhitespace();
        var negative = c == '-';
        if (c is '-' or '+')
        {
            c = ReadByte();
        }

        if (c is < '0' or > '9')
            throw new FormatException("Expected an integer.");

        var val = 0;
        while (c > ' ')
        {
            if (c is < '0' or > '9')
                throw new FormatException("Expected an integer.");

            val = checked(val * 10 - ( c - '0' ));
            c = ReadByte();
        }

        return negative ? val : checked(-val);
    }
}

public static class FileReader
{
    public static async Task ReadAll(string filePath)
    {
        // var text = File.ReadAllText(filePath);
        var text = await File.ReadAllTextAsync(filePath);
        Console.WriteLine(text);
    }

    public static async Task ReadByLine(string filePath)
    {
        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            Console.WriteLine(line);
        }
    }

    public static async Task ReadByByte(string filePath)
    {
        // var bytes = await File.ReadAllBytesAsync(filePath);
        await using var fs = File.OpenRead(filePath);

        var buffer = new byte[4096];

        while (await fs.ReadAsync(buffer, 0, buffer.Length) > 0)
        {
            // process buffer[..n]
        }
    }

    public static async Task ReadByByteMem(string filePath)
    {
        await using var fs = File.OpenRead("input.bin");

        var buffer = new byte[4096];

        while (true)
        {
            var n = await fs.ReadAsync(buffer.AsMemory());
            if (n == 0)
                break;

            // process buffer[..n]
        }
    }
}
