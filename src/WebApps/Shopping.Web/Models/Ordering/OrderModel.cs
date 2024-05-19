namespace Shopping.Web.Models.Ordering;

public record OrderModel(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems
);

public record GetOrderResponse(PaginatedResult<OrderModel> Orders);
public record GetOrderByNameResponse(IEnumerable<OrderModel> Orders);
public record GetOrderByCustomerResponse(IEnumerable<OrderModel> Orders);
public record AddressDto(
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode
);
public record PaymentDto(
    string CardNumber,
    string CardName,
    string Expiration,
    string Cvv,
    int PaymentMethod
);
public record OrderItemDto(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price
);
public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Completed = 3,
    Canceled = 4
}