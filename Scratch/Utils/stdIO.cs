namespace Scratch.Utils;

public class StdRead
{
    /// <summary>
    /// 读取一行字符串
    /// </summary>
    /// <returns>string</returns>
    public string? ReadOneLine()
    {
        var line = Console.ReadLine();

        return line;
    }

    /// <summary>
    /// 按空格读取多个值
    /// </summary>
    /// <returns>(int, long)</returns>
    public (int, long) ReadMultiVal()
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
    public int[] ReadToArr()
    {
        var nums = Console.ReadLine()?
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();

        return nums;
    }

    /// <summary>
    /// 连续读取多行输入（直到 EOF）
    /// </summary>
    public List<string> ReadMultiLines()
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

    public List<string> ReadMultiLinesWithN()
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

class FastScanner
{
    private readonly Stream _stream = Console.OpenStandardInput();
    private readonly byte[] _buffer = new byte[1 << 16];
    private int _len, _ptr;

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

    public int NextInt()
    {
        int c;
        while (( c = ReadByte() ) <= ' ' && c != -1) ;
        int sign = 1;
        if (c == '-')
        {
            sign = -1;
            c = ReadByte();
        }

        int val = 0;
        while (c > ' ')
        {
            val = val * 10 + ( c - '0' );
            c = ReadByte();
        }

        return val * sign;
    }
}
