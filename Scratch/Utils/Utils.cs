using Microsoft.Extensions.Configuration;
using Serilog;

namespace Scratch.Utils;
public class ScratchUtil
{
    /// <summary>
    ///     Initialize Serilog from the appsettings.json file.
    ///     And write log to both the console and log file in directory logs.
    /// </summary>
    public static async ValueTask SerilogInit()
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
}
