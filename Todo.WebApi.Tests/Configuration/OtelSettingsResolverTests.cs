using FluentAssertions;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using Todo.WebApi.Configuration;
using Xunit;

namespace Todo.WebApi.Tests.Configuration;

public sealed class OtelSettingsResolverTests
{
    [Fact]
    public void ResolveResource_Should_Prefer_OtelServiceName_FromEnvironment()
    {
        using var _ = new EnvironmentVariableScope("OTEL_SERVICE_NAME", "todo-webapi-env");

        var resource = new ObservabilityResourceOptions
        {
            ServiceName = "Todo.WebApi", ServiceVersion = "2.0.0", DeploymentEnvironment = "Production"
        };

        var resolved = OtelSettingsResolver.ResolveResource(resource, "Production");

        resolved.ServiceName.Should().Be("todo-webapi-env");
        resolved.ServiceVersion.Should().Be("2.0.0");
        resolved.DeploymentEnvironment.Should().Be("Production");
    }

    [Fact]
    public void ResolveExporterSettings_Should_Prefer_Environment_OverOptions()
    {
        using var _ = new EnvironmentVariableScope("OTEL_EXPORTER_OTLP_ENDPOINT", "http://otel-collector:4318");
        using var __ = new EnvironmentVariableScope(
            "OTEL_EXPORTER_OTLP_PROTOCOL",
            "http/protobuf"
        );

        var options = new OtelOptions { Endpoint = "http://localhost:4317", Protocol = "grpc" };

        var resolved = OtelSettingsResolver.ResolveExporterSettings(options);

        resolved.Endpoint.Should().Be(new Uri("http://otel-collector:4318"));
        resolved.Protocol.Should().Be(OtlpExportProtocol.HttpProtobuf);
    }

    [Fact]
    public void ResolveSampler_Should_Default_To_ParentBasedTraceIdRatio()
    {
        var sampler = OtelSettingsResolver.ResolveSampler(new OtelOptions());

        sampler.Should().BeOfType<ParentBasedSampler>();
    }

    private sealed class EnvironmentVariableScope: IDisposable
    {
        private readonly string name;
        private readonly string? originalValue;

        public EnvironmentVariableScope(string name, string? value)
        {
            this.name = name;
            originalValue = Environment.GetEnvironmentVariable(name);
            Environment.SetEnvironmentVariable(name, value);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(name, originalValue);
        }
    }
}
