using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KidsToyHive.Domain.Models
{
    public class Booking: BaseModel
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }        
        public BookingTimeSlot BookingTimeSlot { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.New;
        public ICollection<BookingDetail> BookingDetails { get; set; } 
            = new HashSet<BookingDetail>();
        public int Cost => BookingDetails.Sum(x => x.Cost);
    }
}
