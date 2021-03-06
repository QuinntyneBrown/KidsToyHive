using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Customer: BaseModel
    {
        public Guid CustomerId { get; set; }
        public Address Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<CustomerLocation> CustomerLocations { get; set; }
            = new HashSet<CustomerLocation>();
        public ICollection<Booking> Bookings { get; set; }
            = new HashSet<Booking>();
        public ICollection<CustomerTermsAndConditions> CustomerTermsAndConditions { get; set; }
            = new HashSet<CustomerTermsAndConditions>();
    }
}
