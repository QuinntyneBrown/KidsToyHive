using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class ProductCreated: DomainEvent
    {
        public ProductCreated(Guid brandId, string name, Guid productId, string description, string imageUrl)
        {
            BrandId = brandId;
            Name = name;
            ProductId = productId;
            Description = description;
            ImageUrl = imageUrl;
        }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
    }
}
