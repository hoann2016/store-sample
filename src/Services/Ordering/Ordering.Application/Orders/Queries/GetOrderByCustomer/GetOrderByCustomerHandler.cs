using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;
using Ordering.Application.Helpers;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System.Linq;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
{
    public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query,
        CancellationToken cancellationToken)
    {
        //var orders = await dbContext.Orders.Include(x => x.OrderItems)
        //    .AsNoTracking()
        //    .Where(x => x.CustomerId == CustomerId.Of(query.CustomerId))
        //    .ToListAsync(cancellationToken);

        var tempOrder = (from o in dbContext.Orders where o.CustomerId == CustomerId.Of(query.CustomerId) select o).ToList();
        var orders = tempOrder
            .Select(o => OrderHelper.CreateOrderDtoFromOrder(o, dbContext));

        return new GetOrderByCustomerResult(orders);
    }
}