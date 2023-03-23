using FluentValidation;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems;

public class RemoveStockValidator : AbstractValidator<RemoveStockRequest>
{
    public RemoveStockValidator()
    {
    }
}
public class RemoveStockRequest : Command<RemoveStockResponse>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public override IEnumerable<string> SideEffects => new List<string>
      {
          $"ProductId {ProductId}"
      };
}
public class RemoveStockResponse
{
    public Guid InventoryItemId { get; set; }
    public int Version { get; set; }
}
public class RemoveStockHandler : IRequestHandler<RemoveStockRequest, RemoveStockResponse>
{
    private readonly IAppDbContext _context;
    public RemoveStockHandler(IAppDbContext context) => _context = context;
    public async Task<RemoveStockResponse> Handle(RemoveStockRequest request, CancellationToken cancellationToken)
    {
        var inventoryItem = await _context.InventoryItems.Where(x => x.ProductId == request.ProductId).SingleAsync();
        if (request.Quantity > inventoryItem.Quantity)
            throw new Exception();
        inventoryItem.Quantity -= request.Quantity;
        await _context.SaveChangesAsync(cancellationToken);
        return new RemoveStockResponse()
        {
            InventoryItemId = inventoryItem.InventoryItemId,
            Version = inventoryItem.Version
        };
    }
}
