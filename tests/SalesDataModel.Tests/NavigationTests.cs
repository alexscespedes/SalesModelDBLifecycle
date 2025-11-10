using System;
using Microsoft.EntityFrameworkCore;
using SalesDataModel.Data;
using SalesDataModel.Models;

namespace SalesDataModel.Tests;

public class NavigationTests
{
    [Fact]
    public void Can_Load_Customer_With_Orders_And_Products()
    {
        var options = new DbContextOptionsBuilder<SalesDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new SalesDbContext(options);

        // Seed
        var customer = new Customer { Name = "John", Email = "john@test.com" };
        var product = new Product { Name = "Laptop", Price = 1000 };
        var order = new Order { Customer = customer, OrderDate = DateTime.UtcNow };
        var item = new OrderItem { Order = order, Product = product, Quantity = 1, UnitPrice = 1000 };
        context.AddRange(customer, product, order, item);
        context.SaveChanges();

        // Act
        var result = context.Customers
            .Include(c => c.Orders)
            .ThenInclude(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .First();

        // Assert
        Assert.Single(result.Orders);
        Assert.Equal("Laptop", result.Orders.First().OrderItems.First().Product!.Name);
    }
}
