using Todo.WebApi.Infrastructure.Validation;
using Todo.WebApi.Orders.Contracts;

namespace Todo.WebApi.Orders;

public static class OrderEndpoints
{
    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/api/orders", CreateOrderAsync)
            .RequireAuthorization()
            .WithTags("Orders")
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized);

        return app;
    }

    private static async Task<IResult> CreateOrderAsync(
        CreateOrderRequest? request,
        OrderCommandService orderCommandService,
        CancellationToken cancellationToken)
    {
        if (!RequestValidation.TryValidate(request, out var validationErrors))
        {
            return Results.ValidationProblem(validationErrors);
        }

        try
        {
            var response = await orderCommandService.CreateOrderAsync(request!, cancellationToken);
            return Results.Ok(response);
        }
        catch
        {
            return Results.Problem(
                title: "Failed to create order.",
                detail: "The order could not be persisted.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
