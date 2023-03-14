using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins;

 public class GetBinByIdRequest : IRequest<GetBinByIdResponse>
 {
     public Guid BinId { get; set; }
 }
 public class GetBinByIdResponse
 {
     public BinDto Bin { get; set; }
 }
 public class GetBinByIdHandler : IRequestHandler<GetBinByIdRequest, GetBinByIdResponse>
 {
     public IAppDbContext _context { get; set; }
     public GetBinByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetBinByIdResponse> Handle(GetBinByIdRequest request, CancellationToken cancellationToken)
         => new GetBinByIdResponse()
         {
             Bin = (await _context.Bins.FindAsync(request.BinId)).ToDto()
         };
 }
