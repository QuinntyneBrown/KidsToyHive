// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.Warehouses;

public class WarehouseDtoValidator : AbstractValidator<WarehouseDto>
{
    public WarehouseDtoValidator()
    {
        RuleFor(x => x.WarehouseId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class WarehouseDto
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class WarehouseExtensions
{
    public static WarehouseDto ToDto(this Warehouse warehouse)
        => new WarehouseDto
        {
            WarehouseId = warehouse.WarehouseId,
            Name = warehouse.Name,
            Version = warehouse.Version
        };
}

