using System;
using SalesDataModel.Models;

namespace SalesDataModel.Data;

public static class DbInitializer
{
    public static void Initialize(SalesDbContext context)
    {
        // Ensure database is created and migrated
        context.Database.EnsureCreated();

        // 1 Categories
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Books" },
                new Category { Name = "Home Appliances" },
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        // 2 Products
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 999.99m },
                new Product { Name = "Smartphone", Price = 699.99m },
                new Product { Name = "C# in Depth", Price = 59.99m },
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            // Assign categories (many-to-many)
            var laptop = context.Products.First(p => p.Name == "Laptop");
            var smartphone = context.Products.First(p => p.Name == "Smartphone");
            var csharpBook = context.Products.First(p => p.Name == "C# in Depth");

            var electronics = context.Categories.First(p => p.Name == "Electronics");
            var books = context.Categories.First(p => p.Name == "Books");

            context.ProductCategories.AddRange(
                new ProductCategory { ProductId = laptop.ProductId, CategoryId = electronics.CategoryId, AssignedtAt = DateTime.UtcNow },
                new ProductCategory { ProductId = smartphone.ProductId, CategoryId = electronics.CategoryId, AssignedtAt = DateTime.UtcNow },
                new ProductCategory { ProductId = csharpBook.ProductId, CategoryId = books.CategoryId, AssignedtAt = DateTime.UtcNow }
            );
            context.SaveChanges();
        }

        // 3 Customers
        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Alice Johnson", Email = "alice@example.com" },
                new Customer {Name = "Bob Smith", Email = "bob@example.com"}
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        // 4 Orders & OrderItems
        if (!context.Orders.Any())
        {
            var alice = context.Customers.First(c => c.Name == "Alice Johnson");
            var laptop = context.Products.First(c => c.Name == "Laptop");
            var book = context.Products.First(c => c.Name == "C# in Depth");

            var order1 = new Order
            {
                CustomerId = alice.CustomerId,
                OrderDate = DateTime.UtcNow.AddDays(-2),
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = laptop.ProductId, Quantity = 1, UnitPrice = laptop.Price },
                    new OrderItem { ProductId = book.ProductId, Quantity = 2, UnitPrice = book.Price },
                }
            };

            context.Orders.Add(order1);
            context.SaveChanges();
        }
    }
}
