// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

public class SalesOrder : BaseModel
{
    public Guid SalesOrderId { get; set; }
    public float Cost { get; set; }
    public SalesOrderStatus Status { get; set; } = SalesOrderStatus.New;

    public ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        = new HashSet<SalesOrderDetail>();
}

