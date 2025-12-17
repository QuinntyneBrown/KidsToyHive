// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class ShipmentSalesOrder
{
    public Guid ShipmentSalesOrderId { get; set; }
    public Guid ShipmentId { get; set; }
    public Guid SalesOrderId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}

