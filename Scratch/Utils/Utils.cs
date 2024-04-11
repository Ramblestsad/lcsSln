using Microsoft.Extensions.Configuration;
using Serilog;

namespace Scratch.Utils;

public static class ScratchUtils
{
    /// <summary>
    ///     Initialize Serilog from the appsettings.json file.
    ///     And write log to both the console and log file in directory logs.
    /// </summary>
    public static void SerilogInit()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./appsettings.json")
            .AddJsonFile(
                $"appsettings." +
                $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}" +
                $".json",
                true)
            .Build();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        Log.Debug("Serilog Initialized");
    }

    private static void FormatWorkFlowBegin(string? flowIdentifier = "Scratch")
    {
        Console.WriteLine();
        Log.Debug($"Scratch//:{flowIdentifier}");
        Console.WriteLine();
    }

    private static void FormatWorkFlowEnd(string? flowIdentifier = "Scratch")
    {
        Console.WriteLine();
        Log.Debug($"Scratch//:End of {flowIdentifier}");
    }

    public static async ValueTask Playground()
    {
        FormatWorkFlowBegin("Playground");
        FormatWorkFlowEnd("Playground");
    }
}
