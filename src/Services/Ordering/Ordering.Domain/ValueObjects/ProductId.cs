namespace Ordering.Domain.ValueObjects;

public class ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId Of(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Product id cannot be empty");
        }
        return new ProductId(productId);
    }
}