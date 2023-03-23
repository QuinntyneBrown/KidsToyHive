using System;

namespace KidsToyHive.Domain.Models;

public class Signature
{
    public Guid SignatureId { get; set; }
    public Guid DigitialAssetId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
