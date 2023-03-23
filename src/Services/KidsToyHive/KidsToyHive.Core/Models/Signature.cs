// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class Signature
{
    public Guid SignatureId { get; set; }
    public Guid DigitialAssetId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}

