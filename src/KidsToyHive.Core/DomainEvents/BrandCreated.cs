using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class BrandCreated: DomainEvent
    {
        public BrandCreated(string name, Guid brandId, string imageUrl)
        {
            Name = name;
            BrandId = brandId;
            ImageUrl = imageUrl;
        }
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public string ImageUrl { get; set; }
    }
}
