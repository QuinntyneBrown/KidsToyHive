using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Warehouses;

 public class GetWarehouseByIdRequest : IRequest<GetWarehouseByIdResponse>
 {
     public Guid WarehouseId { get; set; }
 }
 public class GetWarehouseByIdResponse
 {
     public WarehouseDto Warehouse { get; set; }
 }
 public class GetWarehouseByIdHandler : IRequestHandler<GetWarehouseByIdRequest, GetWarehouseByIdResponse>
 {
     public IAppDbContext _context { get; set; }
     public GetWarehouseByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetWarehouseByIdResponse> Handle(GetWarehouseByIdRequest request, CancellationToken cancellationToken)
         => new GetWarehouseByIdResponse()
         {
             Warehouse = (await _context.Warehouses.FindAsync(request.WarehouseId)).ToDto()
         };
 }
