// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.DigitalAssets;

public class DigitalAssetDtoValidator : AbstractValidator<DigitalAssetDto>
{
    public DigitalAssetDtoValidator()
    {
    }
}
public class DigitalAssetDto
{
    public Guid DigitalAssetId { get; set; }
    public string Name { get; set; }
    public byte[] Bytes { get; set; }
    public string ContentType { get; set; }
}
public static class DigitalAssetExtensions
{
    public static DigitalAssetDto ToDto(this DigitalAsset digitalAsset)
        => new DigitalAssetDto
        {
            DigitalAssetId = digitalAsset.DigitalAssetId,
            Name = digitalAsset.Name,
            Bytes = digitalAsset.Bytes,
            ContentType = digitalAsset.ContentType
        };
}

