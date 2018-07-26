using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class Brand: AggregateRoot
    {
        public Brand(string name, string imageUrl)
            => Apply(new BrandCreated(name, BrandId, imageUrl));

        public Guid BrandId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case BrandCreated brandCreated:
                    Name = brandCreated.Name;
					BrandId = brandCreated.BrandId;
                    break;

                case BrandNameChanged brandNameChanged:
                    Name = brandNameChanged.Name;
                    break;

                case BrandRemoved brandRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new BrandNameChanged(name));

        public void Remove()
            => Apply(new BrandRemoved());
    }
}
