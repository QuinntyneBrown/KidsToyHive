using System;

namespace KidsToyHive.Domain.Models
{
    public class BookingDetail: BaseModel
    {
        public Guid BookingDetailId { get; set; }
        public int Quantity { get; set; }        
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid LocationId { get; set; }
        public Location Location  { get; set; }
        public int Cost { get; set; }
    }
}
