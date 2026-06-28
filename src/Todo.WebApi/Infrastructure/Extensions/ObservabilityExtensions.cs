using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;
using Todo.Observability;
using Todo.WebApi.Infrastructure.Logging;

namespace Todo.WebApi.Infrastructure.Extensions;

public static class ObservabilityExtensions
{
    public static WebApplicationBuilder AddTodoObservability(this WebApplicationBuilder builder)
    {
        var resolvedResource = builder.AddTodoOpenTelemetry(
            "Todo.WebApi",
            tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation(options => options.RecordException = true)
                    .AddHttpClientInstrumentation(options => options.RecordException = true);
            },
            metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation();
            });
        var resolvedObservabilityOptions = new ObservabilityResourceOptions
        {
            ServiceName = resolvedResource.ServiceName,
            ServiceVersion = resolvedResource.ServiceVersion,
            DeploymentEnvironment = resolvedResource.DeploymentEnvironment
        };

        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.With<ActivityContextEnricher>();

            if (context.HostingEnvironment.IsDevelopment())
            {
                configuration.WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] " +
                    "[tr:{trace_id} sp:{span_id}] " +
                    "{SourceContext} {Message:lj}{NewLine}{Exception}");
            }
            else
            {
                configuration.WriteTo.Console(new OtelJsonTextFormatter(resolvedObservabilityOptions));
            }
        });

        return builder;
    }
}
