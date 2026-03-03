using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Todo.WebApi.Configuration;
using Todo.WebApi.Logging;

namespace Todo.WebApi.Extensions;

public static class ObservabilityExtensions
{
    public static WebApplicationBuilder AddTodoObservability(this WebApplicationBuilder builder)
    {
        var configuredObservabilityOptions = builder.Configuration
                                                 .GetSection(ObservabilityResourceOptions.SectionName)
                                                 .Get<ObservabilityResourceOptions>()
                                             ?? new ObservabilityResourceOptions();
        var resolvedResource = OtelSettingsResolver.ResolveResource(
            configuredObservabilityOptions,
            builder.Environment.EnvironmentName);

        var configuredOtelOptions = builder.Configuration
                                        .GetSection(OtelOptions.SectionName)
                                        .Get<OtelOptions>()
                                    ?? new OtelOptions();
        var otlpExporterSettings = OtelSettingsResolver.ResolveExporterSettings(configuredOtelOptions);
        var traceSampler = OtelSettingsResolver.ResolveSampler(configuredOtelOptions);

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
                configuration.WriteTo.Console(new OtelJsonTextFormatter(new ObservabilityResourceOptions
                {
                    ServiceName =
                        resolvedResource.ServiceName,
                    ServiceVersion =
                        resolvedResource.ServiceVersion,
                    DeploymentEnvironment =
                        resolvedResource
                            .DeploymentEnvironment
                }));
            }
        });

        builder.Services.Configure<ObservabilityResourceOptions>(
            builder.Configuration.GetSection(ObservabilityResourceOptions.SectionName));
        builder.Services.PostConfigure<ObservabilityResourceOptions>(options =>
        {
            options.ServiceName = resolvedResource.ServiceName;
            options.ServiceVersion = resolvedResource.ServiceVersion;
            options.DeploymentEnvironment = resolvedResource.DeploymentEnvironment;
        });
        builder.Services.Configure<OtelOptions>(
            builder.Configuration.GetSection(OtelOptions.SectionName));

        builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resourceBuilder =>
            {
                resourceBuilder
                    .AddService(
                        serviceName: resolvedResource.ServiceName,
                        serviceVersion: resolvedResource.ServiceVersion)
                    .AddAttributes(new Dictionary<string, object>
                    {
                        ["deployment.environment"] = resolvedResource.DeploymentEnvironment
                    });
            })
            .WithTracing(tracing =>
            {
                tracing
                    .SetSampler(traceSampler)
                    .AddAspNetCoreInstrumentation(options => options.RecordException = true)
                    .AddHttpClientInstrumentation(options => options.RecordException = true)
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = otlpExporterSettings.Endpoint;
                        options.Protocol = otlpExporterSettings.Protocol;
                        if (!string.IsNullOrWhiteSpace(otlpExporterSettings.Headers))
                        {
                            options.Headers = otlpExporterSettings.Headers;
                        }
                    });
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = otlpExporterSettings.Endpoint;
                        options.Protocol = otlpExporterSettings.Protocol;
                        if (!string.IsNullOrWhiteSpace(otlpExporterSettings.Headers))
                        {
                            options.Headers = otlpExporterSettings.Headers;
                        }
                    });
            });

        return builder;
    }
}
