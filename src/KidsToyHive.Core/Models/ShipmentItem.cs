// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class ShipmentItem : BaseModel
{
    public Guid ShipmentItemId { get; set; }
    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
    public Guid? SalesOrderDetailId { get; set; }
    public Guid? BookingDetailId { get; set; }
    public int Quantity { get; set; }
    public string Comments { get; set; }
}

