using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidation : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidation()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("cart can not be null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("Username required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository basketRepository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        await basketRepository.StoreBasket(cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
}