// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

public class Product : BaseModel
{
    public Guid ProductId { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? ProductCategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; }
        = new HashSet<ProductImage>();
    public string Description { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public Brand Brand { get; set; }
    public int Price { get; set; }
    public int ChargePeriodPrice { get; set; } = 3125;
    public ChargePeriod ChargePeriod = ChargePeriod.Hour;
    public ProductType Type { get; set; } = ProductType.Rental;
}

