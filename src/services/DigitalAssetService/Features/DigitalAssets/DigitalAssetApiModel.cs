using Core.Entities;
using System;

namespace DigitalAssetService.Features.DigitalAssets
{
    public class DigitalAssetApiModel
    {        
        public Guid DigitalAssetId { get; set; }
        public string Name { get; set; }

        public static DigitalAssetApiModel FromDigitalAsset(DigitalAsset digitalAsset)
        {
            var model = new DigitalAssetApiModel();
            model.DigitalAssetId = digitalAsset.DigitalAssetId;
            model.Name = digitalAsset.Name;
            return model;
        }
    }
}
