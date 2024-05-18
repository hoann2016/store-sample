using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
:IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled consumed successfully. Event: {Event}", context.Message.GetType());
        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command, context.CancellationToken);

    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine,
            message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV,
                       message.PaymentMethod);
        var orderId = Guid.NewGuid();
        var orderDto = new OrderDto(orderId, message.CustomerId, message.UserName, addressDto, addressDto, paymentDto,
            Ordering.Domain.OrderStatus.Pending,
            new List<OrderItemDto>()
            {
                new OrderItemDto(orderId,new Guid("61EF3E2C-A2E6-4905-A8D2-2A215D7B5555"),2,200),
                new OrderItemDto(orderId,new Guid("951F50C4-38C3-400B-897B-4075B04215FC"),3,150),
            }
        );
        return new CreateOrderCommand(orderDto);
    }
}