using System;

namespace KidsToyHive.Domain.Models;

public class ProductCategory
{
    public Guid ProductCategoryId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
