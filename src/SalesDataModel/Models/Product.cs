using System;

namespace SalesDataModel.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
