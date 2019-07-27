using System;

namespace KidsToyHive.Domain.Models
{
    public class Tax: BaseModel
    {
        public Guid TaxId { get; set; }
        public int Rate { get; set; }
        public DateTime Effective { get; set; }
    }
}
