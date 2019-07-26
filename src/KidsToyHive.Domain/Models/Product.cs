using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Product: BaseModel
    {
        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
            = new HashSet<ProductImage>();
        public string Description { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public Brand Brand { get; set; }
        public float Price { get; set; }
        public float HourlyRate { get; set; } = 31.25f; 
    }
}
