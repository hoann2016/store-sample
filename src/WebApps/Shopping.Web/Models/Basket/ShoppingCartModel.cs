namespace Shopping.Web.Models.Basket;

public class ShoppingCartModel
{
    public string UserName { get; set; }
    public List<ShoppingCartItemModel> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}

public class ShoppingCartItemModel
{
    public int Quantity { get; set; }
    public string Color { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
}

public record ShoppingCartResponse(ShoppingCartModel Cart);
public record StoreBasketRequest(ShoppingCartModel Cart);
public record StoreBasketResponse(string UserName);
public record DeleteBasketRequest(bool IsSuccess);
public record GetBasketResponse(ShoppingCartModel Cart);
public record DeleteBasketResponse(bool IsSuccess);
public record CheckoutBasketRequest(BasketCheckoutModel BasketCheckoutDto);
public record CheckoutBasketResponse(bool IsSuccess);