using System.Collections.Generic;

namespace Core.Entities
{
    public class ProductCategory: BaseEntity
    {
        public int ProductCategoryId { get; set; }           
		public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
