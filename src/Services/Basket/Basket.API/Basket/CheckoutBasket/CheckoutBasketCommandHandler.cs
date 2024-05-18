using Basket.API.Data;
using Basket.API.DTOs;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidation
    : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidation()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto required");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotNull().WithMessage("User name required");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await basketRepository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        return new CheckoutBasketResult(true);
    }
}