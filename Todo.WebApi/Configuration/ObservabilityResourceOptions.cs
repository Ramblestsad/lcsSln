namespace Todo.WebApi.Configuration;

public sealed class ObservabilityResourceOptions
{
    public const string SectionName = "Observability";

    public string ServiceName { get; set; } = "Todo.WebApi";

    public string? ServiceVersion { get; set; }

    public string DeploymentEnvironment { get; set; } = "Production";
}
