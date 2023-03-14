using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
         RuleFor(request => request.InventoryItemId).NotNull();
     }
 }
 public class RemoveInventoryItemRequest : IRequest
 {
     public Guid InventoryItemId { get; set; }
 }
 public class RemoveInventoryItemHandler : IRequestHandler<Request>
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
