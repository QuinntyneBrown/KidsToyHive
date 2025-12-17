// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Core.Models;

public class CustomerTermsAndConditions
{
    public Guid CustomerTermsAndConditionsId { get; set; }
    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    public DateTime Accepted { get; set; }
    public Customer Customer { get; set; }
}

