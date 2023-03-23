// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Core.Models;

public class BookingDetail : BaseModel
{
    public Guid BookingDetailId { get; set; }
    public int Quantity { get; set; }
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    [ForeignKey("Location")]
    public Guid? LocationId { get; set; }
    public Location Location { get; set; }
    public int Cost { get; set; }
}

