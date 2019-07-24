using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public Address Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<CustomerShipTo> ShipTos { get; set; }
            = new HashSet<CustomerShipTo>();
        public int Version { get; set; }
    }
}
