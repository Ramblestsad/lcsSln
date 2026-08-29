using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Todo.Observability;

public static class ObservabilityBuilderExtensions
{
    public static ResolvedResourceSettings AddTodoOpenTelemetry(
        this IHostApplicationBuilder builder,
        string defaultServiceName,
        Action<TracerProviderBuilder>? configureTracing = null,
        Action<MeterProviderBuilder>? configureMetrics = null)
    {
        var configuredObservabilityOptions = ReadObservabilityOptions(builder.Configuration);
        var resolvedResource = OtelSettingsResolver.ResolveResource(
            configuredObservabilityOptions,
            builder.Environment.EnvironmentName,
            defaultServiceName);

        var configuredOtelOptions = ReadOtelOptions(builder.Configuration);
        var otlpExporterSettings = OtelSettingsResolver.ResolveExporterSettings(configuredOtelOptions);
        var traceSampler = OtelSettingsResolver.ResolveSampler(configuredOtelOptions);

        builder.Logging.Configure(options =>
        {
            options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId;
        });

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
                tracing.SetSampler(traceSampler);
                configureTracing?.Invoke(tracing);

                if (configuredOtelOptions.ExportEnabled)
                {
                    tracing.AddOtlpExporter(options =>
                    {
                        options.Endpoint = otlpExporterSettings.Endpoint;
                        options.Protocol = otlpExporterSettings.Protocol;
                        if (!string.IsNullOrWhiteSpace(otlpExporterSettings.Headers))
                        {
                            options.Headers = otlpExporterSettings.Headers;
                        }
                    });
                }
            })
            .WithMetrics(metrics =>
            {
                configureMetrics?.Invoke(metrics);

                if (configuredOtelOptions.ExportEnabled)
                {
                    metrics.AddOtlpExporter(options =>
                    {
                        options.Endpoint = otlpExporterSettings.Endpoint;
                        options.Protocol = otlpExporterSettings.Protocol;
                        if (!string.IsNullOrWhiteSpace(otlpExporterSettings.Headers))
                        {
                            options.Headers = otlpExporterSettings.Headers;
                        }
                    });
                }
            });

        return resolvedResource;
    }

    private static ObservabilityResourceOptions ReadObservabilityOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection(ObservabilityResourceOptions.SectionName);
        return new ObservabilityResourceOptions
        {
            ServiceName = section["ServiceName"],
            ServiceVersion = section["ServiceVersion"],
            DeploymentEnvironment = section["DeploymentEnvironment"]
        };
    }

    private static OtelOptions ReadOtelOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection(OtelOptions.SectionName);
        var defaults = new OtelOptions();
        return new OtelOptions
        {
            ExportEnabled = ParseBool(section["ExportEnabled"], defaults.ExportEnabled),
            Endpoint = ValueOrDefault(section["Endpoint"], defaults.Endpoint),
            Protocol = ValueOrDefault(section["Protocol"], defaults.Protocol),
            Headers = section["Headers"],
            TraceSampler = ValueOrDefault(section["TraceSampler"], defaults.TraceSampler),
            TraceSamplerArg = ParseDouble(section["TraceSamplerArg"], defaults.TraceSamplerArg)
        };
    }

    private static string ValueOrDefault(string? value, string fallback)
    {
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }

    private static bool ParseBool(string? value, bool fallback)
    {
        return bool.TryParse(value, out var parsed) ? parsed : fallback;
    }

    private static double ParseDouble(string? value, double fallback)
    {
        return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : fallback;
    }
}
