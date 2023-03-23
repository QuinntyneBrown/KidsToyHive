// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Core.Models;

public class Tax : BaseModel
{
    public Guid TaxId { get; set; }
    public double Rate { get; set; }
    public TaxRateType Type { get; set; } = TaxRateType.HST;
    public DateTime Effective { get; set; }
}

