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

public class AddStockValidator : AbstractValidator<AddStockRequest>
{
    public AddStockValidator()
    {
    }
}
public class AddStockRequest : Command<AddStockResponse>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public override IEnumerable<string> SideEffects => new List<string>
      {
          $"ProductId {ProductId}"
      };
}
public class AddStockResponse
{
    public Guid InventoryItemId { get; set; }
    public int Version { get; set; }
}
public class AddStockHandler : IRequestHandler<AddStockRequest, AddStockResponse>
{
    private readonly IAppDbContext _context;
    public AddStockHandler(IAppDbContext context) => _context = context;
    public async Task<AddStockResponse> Handle(AddStockRequest request, CancellationToken cancellationToken)
    {
        var inventoryItem = await _context.InventoryItems.Where(x => x.ProductId == request.ProductId).SingleAsync();
        inventoryItem.Quantity += request.Quantity;
        await _context.SaveChangesAsync(cancellationToken);
        return new AddStockResponse()
        {
            InventoryItemId = inventoryItem.InventoryItemId,
            Version = inventoryItem.Version
        };
    }
}
