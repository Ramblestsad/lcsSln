using System.Globalization;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;

namespace Todo.WebApi.Configuration;

public sealed record ResolvedResourceSettings(
    string ServiceName,
    string ServiceVersion,
    string DeploymentEnvironment);

public sealed record ResolvedOtlpExporterSettings(
    Uri Endpoint,
    OtlpExportProtocol Protocol,
    string? Headers);

public static class OtelSettingsResolver
{
    public static ResolvedResourceSettings ResolveResource(
        ObservabilityResourceOptions options,
        string environmentName)
    {
        var serviceName =
            GetSetting("OTEL_SERVICE_NAME", options.ServiceName)
            ?? "Todo.WebApi";
        var serviceVersion =
            string.IsNullOrWhiteSpace(options.ServiceVersion)
                ? "1.0.0"
                : options.ServiceVersion;
        var deploymentEnvironment =
            string.IsNullOrWhiteSpace(options.DeploymentEnvironment)
                ? environmentName
                : options.DeploymentEnvironment;

        if (string.IsNullOrWhiteSpace(deploymentEnvironment))
        {
            deploymentEnvironment = "Production";
        }

        return new ResolvedResourceSettings(
            serviceName,
            serviceVersion,
            deploymentEnvironment);
    }

    public static ResolvedOtlpExporterSettings ResolveExporterSettings(OtelOptions options)
    {
        var endpointString =
            GetSetting("OTEL_EXPORTER_OTLP_ENDPOINT", options.Endpoint)
            ?? "http://localhost:4317";
        if (!Uri.TryCreate(endpointString, UriKind.Absolute, out var endpoint))
        {
            endpoint = new Uri("http://localhost:4317");
        }

        var protocolValue = GetSetting("OTEL_EXPORTER_OTLP_PROTOCOL", options.Protocol);
        var protocol = ParseProtocol(protocolValue);

        var headers = GetSetting("OTEL_EXPORTER_OTLP_HEADERS", options.Headers);

        return new ResolvedOtlpExporterSettings(endpoint, protocol, headers);
    }

    public static Sampler ResolveSampler(OtelOptions options)
    {
        var samplerName =
            GetSetting("OTEL_TRACES_SAMPLER", options.TraceSampler)
            ?? "parentbased_traceidratio";
        var ratioArg = ParseRatio(
            GetSetting(
                "OTEL_TRACES_SAMPLER_ARG",
                options.TraceSamplerArg.ToString(CultureInfo.InvariantCulture)
            )
        );

        return samplerName.Trim().ToLowerInvariant() switch
        {
            "always_on" => new AlwaysOnSampler(),
            "always_off" => new AlwaysOffSampler(),
            "traceidratio" => new TraceIdRatioBasedSampler(ratioArg),
            "parentbased_always_on" => new ParentBasedSampler(new AlwaysOnSampler()),
            "parentbased_always_off" => new ParentBasedSampler(new AlwaysOffSampler()),
            _ => new ParentBasedSampler(new TraceIdRatioBasedSampler(ratioArg))
        };
    }

    private static string? GetSetting(string envName, string? fallback)
    {
        var envValue = Environment.GetEnvironmentVariable(envName);
        return string.IsNullOrWhiteSpace(envValue) ? fallback : envValue;
    }

    private static OtlpExportProtocol ParseProtocol(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return OtlpExportProtocol.Grpc;
        }

        return value.Trim().ToLowerInvariant() switch
        {
            "http/protobuf" => OtlpExportProtocol.HttpProtobuf,
            "http_protobuf" => OtlpExportProtocol.HttpProtobuf,
            _ => OtlpExportProtocol.Grpc
        };
    }

    private static double ParseRatio(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return 1.0;
        }

        if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var ratio))
        {
            return 1.0;
        }

        return Math.Clamp(ratio, 0.0, 1.0);
    }
}
