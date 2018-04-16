using System.Collections.Generic;

namespace Core.Entities
{
    public class Brand: BaseEntity
    {
        public int BrandId { get; set; }           
		public string Name { get; set; }   
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
