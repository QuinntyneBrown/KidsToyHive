using System;

namespace KidsToyHive.Domain.Models
{
    public class Brand
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Version { get; set; }
    }
}
