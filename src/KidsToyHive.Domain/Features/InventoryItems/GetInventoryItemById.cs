using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems;

 public class GetInventoryItemByIdRequest : IRequest<GetInventoryItemByIdResponse>
 {
     public Guid InventoryItemId { get; set; }
 }
 public class GetInventoryItemByIdResponse
 {
     public InventoryItemDto InventoryItem { get; set; }
 }
 public class GetInventoryItemByIdHandler : IRequestHandler<GetInventoryItemByIdRequest, GetInventoryItemByIdResponse>
 {
     private readonly IAppDbContext _context;
     public GetInventoryItemByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetInventoryItemByIdResponse> Handle(GetInventoryItemByIdRequest request, CancellationToken cancellationToken)
         => new GetInventoryItemByIdResponse()
         {
             InventoryItem = (await _context.InventoryItems.FindAsync(request.InventoryItemId)).ToDto()
         };
 }
