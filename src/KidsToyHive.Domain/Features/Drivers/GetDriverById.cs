using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers;

 public class GetDriverByIdRequest : IRequest<GetDriverByIdResponse>
 {
     public Guid DriverId { get; set; }
 }
 public class GetDriverByIdResponse
 {
     public DriverDto Driver { get; set; }
 }
 public class GetDriverByIdHandler : IRequestHandler<GetDriverByIdRequest, GetDriverByIdResponse>
 {
     private readonly IAppDbContext _context;
     public GetDriverByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetDriverByIdResponse> Handle(GetDriverByIdRequest request, CancellationToken cancellationToken)
         => new GetDriverByIdResponse()
         {
             Driver = (await _context.Drivers.FindAsync(request.DriverId)).ToDto()
         };
 }
