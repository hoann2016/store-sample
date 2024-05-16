using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;

public class InitialData
{
    public static List<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(new Guid("9FC92037-8224-4468-A6CC-BCABA0108F1A")), "Bruce Wayne", "bru@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("102C50C0-C6E8-4E05-9449-1642AC3FF4B7")), "Clark Kent", "ken@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("AAFA6084-5E57-43FB-AC68-E303894488C3")), "Diana Prince", "diana@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("6CD0B387-2B06-4BBB-835F-7782B37156CB")), "Barry Allen", "ball@gmail.com")
    ];

    public static List<Product> Products =>
    [
        Product.Create(ProductId.Of(new Guid("046CB8AE-6FD5-41B0-A0A9-DFBDCED74172")), "Iphone X", 200),
        Product.Create(ProductId.Of(new Guid("951F50C4-38C3-400B-897B-4075B04215FC")), "Iphone 15", 450),
        Product.Create(ProductId.Of(new Guid("B2A7D8A7-3AD7-4EBC-886A-E87D474C639E")), "Iphone 12", 100),
        Product.Create(ProductId.Of(new Guid("61EF3E2C-A2E6-4905-A8D2-2A215D7B5555")), "Iphone Pro 11", 200)
    ];

    public static List<Order> Orders
    {
        get
        {
            var address1 = Address.Of("Ricky", "Redmond", "test@test.com", "123 st road 12", "vn", "70000", "123");
            var address2 = Address.Of("Ricky", "Redmond", "test2@gmail.com", "234434 ", "us", "0", "82");
            var payment1 = Payment.Of("Ricky Nguyen", "123456789", "2223", "123", 1);
            var payment2 = Payment.Of("Ricky Nguyen2", "13123123", "23123", "456", 2);
            var order1 = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(new Guid("9FC92037-8224-4468-A6CC-BCABA0108F1A")), OrderName.Of("Order1"),
                shippingAddress: address1, billingAddress: address1, payment1);
            var order2 = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(new Guid("102C50C0-C6E8-4E05-9449-1642AC3FF4B7")), OrderName.Of("Order2"),
                shippingAddress: address2, billingAddress: address2, payment2);
            order1.Add(ProductId.Of(new Guid("046CB8AE-6FD5-41B0-A0A9-DFBDCED74172")), 5, 100);
            order1.Add(ProductId.Of(new Guid("951F50C4-38C3-400B-897B-4075B04215FC")), 15, 2);
            order2.Add(ProductId.Of(new Guid("B2A7D8A7-3AD7-4EBC-886A-E87D474C639E")), 53, 100);
            order2.Add(ProductId.Of(new Guid("61EF3E2C-A2E6-4905-A8D2-2A215D7B5555")), 135, 2);

            return [order1, order2];
        }
    }
}