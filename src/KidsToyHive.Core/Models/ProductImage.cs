// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class ProductImage : BaseModel
{
    public Guid ProductImageId { get; set; }
    public Guid ProductId { get; set; }
    public Guid DigitalAssetId { get; set; }
    public DigitalAsset DigitalAsset { get; set; }
    public string Url
    {
        get
        {
            return $"api/digitalassets/serve/file/{DigitalAsset.Name}";
        }
    }
}

