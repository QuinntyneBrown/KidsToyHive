// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.InventoryItems;

public class RemoveInventoryItemValidator : AbstractValidator<RemoveInventoryItemRequest>
{
    public RemoveInventoryItemValidator()
    {
        RuleFor(request => request.InventoryItemId).NotNull();
    }
}
public class RemoveInventoryItemRequest : IRequest
{
    public Guid InventoryItemId { get; set; }
}
public class RemoveInventoryItemHandler : IRequestHandler<RemoveInventoryItemRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveInventoryItemHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveInventoryItemRequest request, CancellationToken cancellationToken)
    {
        var inventoryItem = await _context.InventoryItems.FindAsync(request.InventoryItemId);
        _context.InventoryItems.Remove(inventoryItem);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

