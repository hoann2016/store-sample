using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger,IPublishEndpoint publishEndpoint, IFeatureManager featureManager)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event handled {DomainEvent}", domainEvent.GetType());
        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderCratedIntegrationEvent = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderCratedIntegrationEvent, cancellationToken);
        }
    }
}