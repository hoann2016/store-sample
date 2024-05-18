using Ordering.Application.DTOs;
using Ordering.Domain.Models;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(order.Id.Value, order.CustomerId.Value, order.OrderName.Value,
            new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress
                , order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State,
                order.ShippingAddress.ZipCode),
            new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress
                , order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State,
                order.ShippingAddress.ZipCode),
            new PaymentDto(order.Payment.CardNumber,order.Payment.CardName, order.Payment.Expiration,
                order.Payment.CVV, order.Payment.PaymentMethod),
            order.Status,
            order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price))
                .ToList()
        );
    }

    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order =>
        {
            return new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                    AddressLine: order.ShippingAddress.AddressLine,
                    Country: order.ShippingAddress.Country,
                    State: order.ShippingAddress.State,
                    ZipCode: order.ShippingAddress.ZipCode,
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    EmailAddress: order.ShippingAddress.EmailAddress
                ),
                BillingAddress: new AddressDto(
                    AddressLine: order.ShippingAddress.AddressLine,
                    Country: order.ShippingAddress.Country,
                    State: order.ShippingAddress.State,
                    ZipCode: order.ShippingAddress.ZipCode,
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    EmailAddress: order.ShippingAddress.EmailAddress
                ),
                Payment: new PaymentDto(
                    CardNumber: order.Payment.CardNumber,
                    CardName: order.Payment.CardName,
                    Expiration: order.Payment.Expiration,
                    Cvv: order.Payment.CVV,
                    PaymentMethod: order.Payment.PaymentMethod
                ),
                OrderItems: order.OrderItems.Select(x =>
                    {
                        return new OrderItemDto(
                            ProductId: x.ProductId.Value,
                            OrderId: x.OrderId.Value,
                            Price: x.Price,
                            Quantity: x.Quantity
                        );
                    }
                ).ToList(),
                Status: order.Status
            );
        });
    }
}