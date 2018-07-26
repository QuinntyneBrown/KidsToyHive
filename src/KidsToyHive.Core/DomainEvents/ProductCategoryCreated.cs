using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class ProductCategoryCreated: DomainEvent
    {
        public ProductCategoryCreated(string name, Guid productCategoryId)
        {
            Name = name;
            ProductCategoryId = productCategoryId;
        }
        public string Name { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
