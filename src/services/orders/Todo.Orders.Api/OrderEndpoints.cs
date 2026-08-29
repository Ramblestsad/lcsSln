using Todo.Orders.Contracts;

namespace Todo.Orders.Api;

public static class OrderEndpoints
{
    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders")
            .RequireAuthorization();

        group.MapPost("/", CreateOrderAsync)
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>()
            .ProducesValidationProblem();
        group.MapGet("/{id:guid}", GetOrderAsync)
            .WithName("GetOrder")
            .Produces<OrderDetailsResponse>()
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }

    private static async Task<IResult> CreateOrderAsync(
        CreateOrderRequest? request,
        OrderService orderService,
        CancellationToken cancellationToken)
    {
        if (!RequestValidation.TryValidate(request, out var errors))
        {
            return Results.ValidationProblem(errors);
        }

        var response = await orderService.CreateAsync(request!, cancellationToken);
        return Results.Created($"/api/orders/{response.OrderId}", response);
    }

    private static async Task<IResult> GetOrderAsync(
        Guid id,
        OrderService orderService,
        CancellationToken cancellationToken)
    {
        var response = await orderService.GetAsync(id, cancellationToken);
        return response is null ? Results.NotFound() : Results.Ok(response);
    }
}
