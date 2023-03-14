using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentSalesOrders;

 public class GetShipmentSalesOrderByIdRequest : IRequest<GetShipmentSalesOrderByIdResponse>
 {
     public Guid ShipmentSalesOrderId { get; set; }
 }
 public class GetShipmentSalesOrderByIdResponse
 {
     public ShipmentSalesOrderDto ShipmentSalesOrder { get; set; }
 }
 public class GetShipmentSalesOrderByIdHandler : IRequestHandler<GetShipmentSalesOrderByIdRequest, GetShipmentSalesOrderByIdResponse>
 {
     private readonly IAppDbContext _context;
     public GetShipmentSalesOrderByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetShipmentSalesOrderByIdResponse> Handle(GetShipmentSalesOrderByIdRequest request, CancellationToken cancellationToken)
         => new GetShipmentSalesOrderByIdResponse()
         {
             ShipmentSalesOrder = (await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrderId)).ToDto()
         };
 }
