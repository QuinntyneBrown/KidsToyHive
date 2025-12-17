// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;

namespace KidsToyHive.Core.Features.Bins;

public static class BinExtensions
{
    public static BinDto ToDto(this Bin bin)
        => new BinDto
        {
            BinId = bin.BinId,
            Name = bin.Name,
            Version = bin.Version
        };
}

