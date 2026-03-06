using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Todo.WebApi.Configuration;
using Xunit;

namespace Todo.WebApi.Tests.Configuration;

public sealed class PostgresConnectionStringResolverTests
{
    [Fact]
    public void Resolve_Should_Throw_When_WriteConnectionString_IsMissing()
    {
        var configuration = BuildConfiguration(
            new Dictionary<string, string?>
            {
                ["ConnectionStrings:postgresRead"] = "Host=read;"
            });

        var action = () => PostgresConnectionStringResolver.Resolve(configuration);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*postgresWrite*");
    }

    [Fact]
    public void Resolve_Should_Throw_When_ReadConnectionString_IsMissing()
    {
        var configuration = BuildConfiguration(
            new Dictionary<string, string?>
            {
                ["ConnectionStrings:postgresWrite"] = "Host=write;"
            });

        var action = () => PostgresConnectionStringResolver.Resolve(configuration);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*postgresRead*");
    }

    [Fact]
    public void Resolve_Should_Return_Write_And_ReadConnectionStrings()
    {
        var configuration = BuildConfiguration(
            new Dictionary<string, string?>
            {
                ["ConnectionStrings:postgresWrite"] = "Host=write;",
                ["ConnectionStrings:postgresRead"] = "Host=read;"
            });

        var resolved = PostgresConnectionStringResolver.Resolve(configuration);

        resolved.WriteConnectionString.Should().Be("Host=write;");
        resolved.ReadConnectionString.Should().Be("Host=read;");
    }

    private static IConfiguration BuildConfiguration(Dictionary<string, string?> values)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(values)
            .Build();
    }
}
