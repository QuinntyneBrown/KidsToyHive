using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Domain.Models
{
    public class CustomerTermsAndConditions
    {
        public Guid CustomerTermsAndConditionsId { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public DateTime Accepted { get; set; }
        public Customer Customer { get; set; }
    }
}
