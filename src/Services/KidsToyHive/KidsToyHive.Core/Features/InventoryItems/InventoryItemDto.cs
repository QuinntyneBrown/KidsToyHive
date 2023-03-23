// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.InventoryItems;

public class InventoryItemDtoValidator : AbstractValidator<InventoryItemDto>
{
    public InventoryItemDtoValidator()
    {
        RuleFor(x => x.InventoryItemId).NotNull();
    }
}
public class InventoryItemDto
{
    public Guid InventoryItemId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public int Version { get; set; }
}
public static class InventoryItemExtensions
{
    public static InventoryItemDto ToDto(this InventoryItem inventoryItem)
        => new InventoryItemDto
        {
            InventoryItemId = inventoryItem.InventoryItemId,
            Version = inventoryItem.Version
        };
}

