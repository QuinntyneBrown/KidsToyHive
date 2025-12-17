// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

public class Brand : BaseModel
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<Product> Products { get; set; }
        = new HashSet<Product>();
}

