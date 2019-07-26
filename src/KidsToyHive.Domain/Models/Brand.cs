using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Brand: BaseModel
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; }
            = new HashSet<Product>();        
    }
}
