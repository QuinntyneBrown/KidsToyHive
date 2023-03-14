using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.DigitalAssets;

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
