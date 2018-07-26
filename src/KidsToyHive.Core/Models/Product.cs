using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class Product: AggregateRoot
    {
        public Product(Guid brandId, string name, string description, string imageUrl)
            => Apply(new ProductCreated(brandId, name,ProductId, description,imageUrl));

        public Guid ProductId { get; set; } = Guid.NewGuid();
        public Guid? ProductCategoryId { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case ProductCreated productCreated:
                    BrandId = productCreated.BrandId;
                    Name = productCreated.Name;
					ProductId = productCreated.ProductId;
                    Description = productCreated.Description;
                    ImageUrl = productCreated.ImageUrl;
                    break;

                case ProductNameChanged productNameChanged:
                    Name = productNameChanged.Name;
                    break;

                case ProductRemoved productRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new ProductNameChanged(name));

        public void Remove()
            => Apply(new ProductRemoved());
    }
}
