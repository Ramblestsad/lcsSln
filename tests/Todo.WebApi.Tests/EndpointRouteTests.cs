using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Todo.WebApi.Tests;

public sealed class EndpointRouteTests(TodoWebApiFactory factory): IClassFixture<TodoWebApiFactory>
{
    [Fact]
    public void App_Should_Map_Legacy_Http_Routes()
    {
        var endpoints = factory.Services.GetRequiredService<EndpointDataSource>()
            .Endpoints
            .OfType<RouteEndpoint>()
            .Select(endpoint => endpoint.RoutePattern.RawText)
            .ToArray();

        endpoints.Should().Contain("/api/Auth/register");
        endpoints.Should().Contain("/api/Auth/login");
        endpoints.Should().Contain("/api/TodoItems");
        endpoints.Should().Contain("/api/TodoItems/{id:long}");
        endpoints.Should().Contain("/api/orders");
        endpoints.Should().Contain("/api/todo-engagement/hot/{todoId:long}/view");
        endpoints.Should().Contain("/hubs/chat");
    }
}
