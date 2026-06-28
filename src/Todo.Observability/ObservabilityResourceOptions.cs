namespace Todo.Observability;

public sealed class ObservabilityResourceOptions
{
    public const string SectionName = "Observability";

    public string? ServiceName { get; set; }

    public string? ServiceVersion { get; set; }

    public string? DeploymentEnvironment { get; set; }
}
