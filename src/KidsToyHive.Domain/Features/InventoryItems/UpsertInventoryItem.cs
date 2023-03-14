using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
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
     private readonly IAppDbContext _context;
     public UpsertInventoryItemHandler(IAppDbContext context) => _context = context;
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
