

using MediatR;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Helpers;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        var tempOrder = (from o in dbContext.Orders where o.OrderName.Value == query.Name select o).ToList();
        var orders = tempOrder
            .Select(o => OrderHelper.CreateOrderDtoFromOrder(o,dbContext));

        return new GetOrderByNameResult(orders);
    }
    
}