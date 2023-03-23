using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.InventoryItems;

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
