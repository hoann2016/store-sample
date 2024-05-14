using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}   
public class DeleteProductCommandHandler(IDocumentSession documentSession)
    :
        ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = documentSession.Load<Product>(command.Id);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        documentSession.Delete<Product>(command.Id);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}