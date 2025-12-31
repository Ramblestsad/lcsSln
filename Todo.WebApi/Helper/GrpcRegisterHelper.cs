using Todo.WebApi.Grpc.Greeter;

namespace Todo.WebApi.Helper;

public static class GrpcRegistrationExtension
{
    public static IEndpointRouteBuilder MapGrpcServices(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGrpcService<GreeterService>();

        return endpoints;
    }
}
