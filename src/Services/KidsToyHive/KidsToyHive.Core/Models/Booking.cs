// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KidsToyHive.Core.Models;

public class Booking : BaseModel
{
    public Guid BookingId { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string Name { get; set; }
    [ForeignKey("Location")]
    public Guid? LocationId { get; set; }
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

