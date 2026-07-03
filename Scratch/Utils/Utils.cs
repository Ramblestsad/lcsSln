using Serilog;
using Serilog.Events;

namespace Scratch.Utils;

public static class ScratchUtils
{
    /// <summary>
    ///     Initialize Serilog.
    /// </summary>
    public static void SerilogInit()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Default", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .Destructure.ToMaximumDepth(4)
            .Destructure.ToMaximumStringLength(100)
            .Destructure.ToMaximumCollectionCount(10)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "Scratch")
            .WriteTo.Console()
            .CreateLogger();
    }
}
