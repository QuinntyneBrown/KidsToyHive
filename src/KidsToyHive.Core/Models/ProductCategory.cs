using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class ProductCategory: AggregateRoot
    {
        public ProductCategory(string name)
            => Apply(new ProductCategoryCreated(name,ProductCategoryId));

        public Guid ProductCategoryId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case ProductCategoryCreated productCategoryCreated:
                    Name = productCategoryCreated.Name;
					ProductCategoryId = productCategoryCreated.ProductCategoryId;
                    break;

                case ProductCategoryNameChanged productCategoryNameChanged:
                    Name = productCategoryNameChanged.Name;
                    break;

                case ProductCategoryRemoved productCategoryRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new ProductCategoryNameChanged(name));

        public void Remove()
            => Apply(new ProductCategoryRemoved());
    }
}
