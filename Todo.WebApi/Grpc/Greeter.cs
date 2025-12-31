using Grpc.Core;

namespace Todo.WebApi.Grpc.Greeter;

public class GreeterService: Greeter.GreeterBase
{
    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return new HelloReply { Message = $"Hello {request.Name}" };
    }
}
