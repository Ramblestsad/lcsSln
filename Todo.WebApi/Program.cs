using Serilog;

namespace Todo.WebApi;

internal class Program {
    public static async Task Main(string[] args) {
        Log.Logger = new LoggerConfiguration()
            .CreateBootstrapLogger();

        var host = CreateHostBuilder(args).Build();

        Log.Information("Serilog initialized");
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((context, services, configuration) => {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}
