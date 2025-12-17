// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.InventoryItems;

public class UpsertInventoryItemValidator : AbstractValidator<UpsertInventoryItemRequest>
{
    public UpsertInventoryItemValidator()
    {
        RuleFor(request => request.InventoryItem).NotNull();
        RuleFor(request => request.InventoryItem).SetValidator(new InventoryItemDtoValidator());
    }
}
public class UpsertInventoryItemRequest : IRequest<UpsertInventoryItemResponse>
{
    public InventoryItemDto InventoryItem { get; set; }
}
public class UpsertInventoryItemResponse
{
    public Guid InventoryItemId { get; set; }
}
public class UpsertInventoryItemHandler : IRequestHandler<UpsertInventoryItemRequest, UpsertInventoryItemResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertInventoryItemHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertInventoryItemResponse> Handle(UpsertInventoryItemRequest request, CancellationToken cancellationToken)
    {
        var inventoryItem = await _context.InventoryItems.FindAsync(request.InventoryItem.InventoryItemId);
        if (inventoryItem == null)
        {
            inventoryItem = new InventoryItem();
            _context.InventoryItems.Add(inventoryItem);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertInventoryItemResponse() { InventoryItemId = inventoryItem.InventoryItemId };
    }
}

