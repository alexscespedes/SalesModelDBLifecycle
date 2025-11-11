using System;
using SalesDataModel.Models;

namespace SalesDataModel.Data;

public class DataSeeder
{
    public static async Task SeedAsync(SalesDbContext context, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
                {
                    new () {Name = "Alexander Sencion", Email = "alex@example.com"},
                    new () {Name = "Jim Halpert", Email = "jim@example.com"},
                };

                context.Customers.AddRange(customers);
                await context.SaveChangesAsync();
            }

            if (!context.Orders.Any())
            {
                var alex = context.Customers.First();
                var laptop = context.Products.First();

                var order = new Order
                {
                    CustomerId = alex.CustomerId,
                    OrderDate = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>
                    {
                        new () { ProductId = laptop.ProductId, Quantity = 1, UnitPrice = laptop.Price }
                    }
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }
        }
    }
}
