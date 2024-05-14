namespace Catalog.Api.Products.GetProductsByCategory;
public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public class GetProductsByCategoryQueryHandler(IDocumentSession documentSession):
    IQueryHandler<GetProductsByCategoryQuery,GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
       var products =await documentSession.Query<Product>().Where(p => p.Category.Contains(request.Category)).ToListAsync(cancellationToken);
        return new GetProductsByCategoryResult(products);
    }
}