namespace Todo.WebApi.Configuration;

public sealed class OtelOptions
{
    public const string SectionName = "OpenTelemetry";

    // Environment variables (OTEL_*) take precedence over these defaults.
    public string Endpoint { get; set; } = "http://localhost:4317";

    public string Protocol { get; set; } = "grpc";

    public string? Headers { get; set; }

    public string TraceSampler { get; set; } = "parentbased_traceidratio";

    public double TraceSamplerArg { get; set; } = 1.0;
}
