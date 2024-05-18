using BuildingBlocks.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;
using Ordering.Application.Helpers;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrderHandler(IApplicationDbContext dbContext) :
    IRequestHandler<GetOrdersQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var count = await dbContext.Orders.LongCountAsync(cancellationToken: cancellationToken);
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;


            var tempOrder = (from o in dbContext.Orders select o).ToList();
            var orders = tempOrder
                .Select(o => OrderHelper.CreateOrderDtoFromOrder(o, dbContext));

            return new GetOrderResult(new PaginationResult<OrderDto>(
                pageIndex,
                pageSize,
                count,
                data: orders.ToList().OrderBy(s => s.OrderName).Skip(pageIndex * pageSize)
                    .Take(query.PaginationRequest.PageSize)
            ));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}