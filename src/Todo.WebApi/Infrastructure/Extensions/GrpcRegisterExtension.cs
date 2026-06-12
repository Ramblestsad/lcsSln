using Todo.WebApi.Greeter;

namespace Todo.WebApi.Infrastructure.Extensions;

public static class GrpcRegistrationExtension
{
    public static IEndpointRouteBuilder MapGrpcServices(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGrpcService<GreeterService>();

        return endpoints;
    }
}
