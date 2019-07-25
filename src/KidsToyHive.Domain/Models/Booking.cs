using System;
using System.Collections;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Booking: BaseModel
    {
        public Guid BookingId { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }
        = new HashSet<BookingDetail>();
        public float Cost { get; set; }
    }
}
