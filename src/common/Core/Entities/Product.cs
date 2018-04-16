using System.Collections.Generic;

namespace Core.Entities
{
    public class Product: BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int ProductCategoryId { get; set; }
        public int BrandId { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
        public ProductCategory ProductCategory { get; set; }
        public Brand Brand { get; set; }
    }
}
