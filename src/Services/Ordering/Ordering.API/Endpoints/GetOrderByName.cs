using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints;

public record GetOrderByNameResponse(List<OrderDto> Orders);

public class GetOrderByName : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByNameQuery(orderName));
            var response = result.Adapt<GetOrderByNameResponse>();
            return Results.Ok(result);
        }).WithName("GetOrderByName")
        .Produces<GetOrderByNameResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Order By Name")
        .WithDescription("Get Order By Name");
    }
}