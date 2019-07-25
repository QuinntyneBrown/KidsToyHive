using KidsToyHive.Core.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Booking: BaseModel
    {
        public Guid BookingId { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingTimeSlot BookingTimeSlot { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }
        = new HashSet<BookingDetail>();
        public float Cost { get; set; }
    }
}
