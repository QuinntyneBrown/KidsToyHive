using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class DigitalAsset : AggregateRoot
    {
        public DigitalAsset(string name, byte[] bytes, string contentType)
            => Apply(new DigitalAssetCreated(DigitalAssetId, name, bytes, contentType));

        public Guid DigitalAssetId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }

        protected override void When(DomainEvent @event)
        {
            switch(@event)
            {
                case DigitalAssetCreated digitalAssetCreated:
                    DigitalAssetId = digitalAssetCreated.DigitalAssetId;
                    Bytes = digitalAssetCreated.Bytes;
                    ContentType = digitalAssetCreated.ContentType;
                    Name = digitalAssetCreated.Name;
                    break;
            }            
        }

        protected override void EnsureValidState()
        {

        }
    }
}
