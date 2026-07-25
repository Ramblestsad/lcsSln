using System.Text;

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
        using var reader = new StreamReader(Console.OpenStandardInput());
        var lines = new List<string>();

        string? line;
        while (( line = reader.ReadLine() ) != null)
        {
            lines.Add(line);
        }

        return lines;
    }

    public static List<string> ReadMultiLinesWithN()
    {
        using var reader = new StreamReader(Console.OpenStandardInput());
        var n = int.Parse(reader.ReadLine()!);
        var lines = new List<string>();

        for (var i = 0; i < n; i++)
        {
            var line = reader.ReadLine()!;
            lines.Add(line);
        }

        return lines;
    }
}

public static class ACMScan
{
    /// <summary>
    /// 普通 ACM 输入：先熟悉 Console.ReadLine + Split 写法。
    /// </summary>
    public static class Basic
    {
        public static int ReadInt(TextReader? input = null) =>
            int.Parse(( input ?? Console.In ).ReadLine()!);

        public static int[] ReadIntArray(TextReader? input = null) =>
            ( input ?? Console.In ).ReadLine()!
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }

    /// <summary>
    /// 大量数据输入：用字节缓冲区按空白分隔读取。
    /// </summary>
    public sealed class Fast
    {
        private readonly Stream _input;
        private readonly byte[] _buffer = new byte[1 << 16];
        private int _length;
        private int _position;

        public Fast(Stream? input = null)
        {
            _input = input ?? Console.OpenStandardInput();
        }

        private int ReadByte()
        {
            if (_position < _length)
                return _buffer[_position++];

            _length = _input.Read(_buffer, 0, _buffer.Length);
            _position = 0;
            return _length == 0 ? -1 : _buffer[_position++];
        }

        private int NextNonWhitespace()
        {
            int c;
            while (( c = ReadByte() ) <= ' ' && c != -1) { }
            return c;
        }

        public string Next()
        {
            var c = NextNonWhitespace();
            if (c == -1)
                throw new EndOfStreamException();

            var token = new StringBuilder();
            while (c > ' ')
            {
                token.Append((char)c);
                c = ReadByte();
            }

            return token.ToString();
        }

        public int NextInt() => checked((int)NextLong());

        public long NextLong()
        {
            var c = NextNonWhitespace();
            if (c == -1)
                throw new EndOfStreamException();

            var negative = c == '-';
            if (c is '-' or '+')
                c = ReadByte();

            if (c is < '0' or > '9')
                throw new FormatException("Expected an integer.");

            long value = 0;
            while (c > ' ')
            {
                if (c is < '0' or > '9')
                    throw new FormatException("Expected an integer.");

                value = checked(value * 10 - ( c - '0' ));
                c = ReadByte();
            }

            return negative ? value : checked(-value);
        }
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
