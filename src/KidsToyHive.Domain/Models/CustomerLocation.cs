using System;

namespace KidsToyHive.Domain.Models
{
    public class CustomerLocation: BaseModel
    {
        public Guid CustomerLocationId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid LocationId { get; set; }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
    }
}
