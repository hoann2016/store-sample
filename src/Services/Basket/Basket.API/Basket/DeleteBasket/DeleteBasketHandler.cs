using Basket.API.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket;
public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidation : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidation()
    {
        RuleFor(x => x.UserName).NotNull().WithMessage("Username required");
    }
}
public class DeleteBasketHandler(IBasketRepository basketRepository):
    ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}