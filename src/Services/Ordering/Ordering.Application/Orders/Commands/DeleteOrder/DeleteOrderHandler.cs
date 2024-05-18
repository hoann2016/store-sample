using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderCommandResult>
{
    public async Task<DeleteOrderCommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
        if (order == null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderCommandResult(true);
    }
}