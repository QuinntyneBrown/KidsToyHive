using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Domain.Models
{
    public class Tax: BaseModel
    {
        public Guid TaxId { get; set; }
        public decimal Rate { get; set; }
        public TaxRateType Type { get; set; } = TaxRateType.HST;
        public DateTime Effective { get; set; }
    }
}
