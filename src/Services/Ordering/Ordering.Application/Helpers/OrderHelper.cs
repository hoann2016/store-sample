using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Domain.Models;

namespace Ordering.Application.Helpers;

public  static class OrderHelper
{
    public static OrderDto CreateOrderDtoFromOrder(Order o, IApplicationDbContext dbContext)
    {
        return new OrderDto(
            o.Id.Value,
            o.CustomerId.Value,
            o.OrderName.Value,
            new AddressDto(
                o.BillingAddress.FirstName,
                o.BillingAddress.LastName,
                o.BillingAddress.EmailAddress,
                o.BillingAddress.AddressLine,
                o.BillingAddress.Country,
                o.BillingAddress.State,
                o.BillingAddress.ZipCode
            ),
            new AddressDto(
                o.BillingAddress.FirstName,
                o.BillingAddress.LastName,
                o.BillingAddress.EmailAddress,
                o.BillingAddress.AddressLine,
                o.BillingAddress.Country,
                o.BillingAddress.State,
                o.BillingAddress.ZipCode
            ),
            new PaymentDto(
                o.Payment.CardNumber,
                o.Payment.CardName,
                o.Payment.Expiration,
                o.Payment.CVV,
                o.Payment.PaymentMethod
            ),
            o.Status,
            (from oi in dbContext.OrderItems.Where(x => o.Id == x.OrderId)
                select new OrderItemDto(
                    oi.OrderId.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price
                )
            ).ToList()

        );
    }  
}