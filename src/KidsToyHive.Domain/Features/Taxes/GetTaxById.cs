using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Taxes;

 public class GetTaxByIdRequest : IRequest<GetTaxByIdResponse>
 {
     public Guid TaxId { get; set; }
 }
 public class GetTaxByIdResponse
 {
     public TaxDto Tax { get; set; }
 }
 public class GetTaxByIdHandler : IRequestHandler<GetTaxByIdRequest, GetTaxByIdResponse>
 {
     public IAppDbContext _context { get; set; }
     public GetTaxByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetTaxByIdResponse> Handle(GetTaxByIdRequest request, CancellationToken cancellationToken)
         => new GetTaxByIdResponse()
         {
             Tax = (await _context.Taxes.FindAsync(request.TaxId)).ToDto()
         };
 }
