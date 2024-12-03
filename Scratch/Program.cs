using Scratch.Utils;
using Serilog;

internal class Program
{
    public static void Main(string[] args)
    {
        Log.Information("Entry point");

        ScratchUtils.SerilogInit();
    }
}
