using System;

namespace SalesDataModel.Models;

public class ProductCategory
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public DateTime AssignedtAt { get; set; } = DateTime.UtcNow;
    public Product? Product { get; set; }
    public Category? Category { get; set; }
}
