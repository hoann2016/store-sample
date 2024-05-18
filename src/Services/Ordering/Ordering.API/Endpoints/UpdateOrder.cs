using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdaterOrderRequest(OrderDto Order);
public record UpdaterOrderResponse(bool IsSuccess);
public class UpdateOrder:CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdaterOrderRequest order, ISender sender) =>
        {
            var command = order.Adapt<UpdateOrderCommand>();
            var result = await sender.Send(command, default);
            var response = result.Adapt<UpdaterOrderResponse>();
            return Results.Ok(response);
        }).WithName("UpdatedOrder")
        .Produces<UpdaterOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Updated Order")
        .WithDescription("Updated Order");
    }
}