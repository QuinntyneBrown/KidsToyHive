using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems;

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
    private readonly IAppDbContext _context;
    public RemoveInventoryItemHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveInventoryItemRequest request, CancellationToken cancellationToken)
    {
        var inventoryItem = await _context.InventoryItems.FindAsync(request.InventoryItemId);
        _context.InventoryItems.Remove(inventoryItem);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
