using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems;

 public class GetShipmentItemByIdRequest : IRequest<GetShipmentItemByIdResponse>
 {
     public Guid ShipmentItemId { get; set; }
 }
 public class GetShipmentItemByIdResponse
 {
     public ShipmentItemDto ShipmentItem { get; set; }
 }
 public class GetShipmentItemByIdHandler : IRequestHandler<GetShipmentItemByIdRequest, GetShipmentItemByIdResponse>
 {
     public IAppDbContext _context { get; set; }
     public GetShipmentItemByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetShipmentItemByIdResponse> Handle(GetShipmentItemByIdRequest request, CancellationToken cancellationToken)
         => new GetShipmentItemByIdResponse()
         {
             ShipmentItem = (await _context.ShipmentItems.FindAsync(request.ShipmentItemId)).ToDto()
         };
 }
