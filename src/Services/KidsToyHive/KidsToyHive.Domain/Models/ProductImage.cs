using System;

namespace KidsToyHive.Domain.Models;

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
