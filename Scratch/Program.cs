using Scratch.Utils;
using Serilog;

internal class Program
{
    public static void Main(string[] args)
    {
        ScratchUtils.SerilogInit();
        Log.Information("Entry point");
    }
}
