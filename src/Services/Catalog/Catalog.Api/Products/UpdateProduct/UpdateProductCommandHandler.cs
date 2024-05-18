﻿using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price):ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2,150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price greater than 0");
    }
}   
internal class UpdateProductCommandHandler(IDocumentSession documentSession)
: ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = documentSession.Load<Product>(command.Id);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        documentSession.Update(product);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}