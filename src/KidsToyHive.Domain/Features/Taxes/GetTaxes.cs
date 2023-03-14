using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Taxes;

public class GetTaxesRequest : IRequest<GetTaxesResponse> { }
public class GetTaxesResponse
{
    public IEnumerable<TaxDto> Taxes { get; set; }
}
public class GetTaxesHandler : IRequestHandler<GetTaxesRequest, GetTaxesResponse>
{
    private readonly IAppDbContext _context;
    public GetTaxesHandler(IAppDbContext context) => _context = context;
    public async Task<GetTaxesResponse> Handle(GetTaxesRequest request, CancellationToken cancellationToken)
        => new GetTaxesResponse()
        {
            Taxes = await _context.Taxes.Select(x => x.ToDto()).ToArrayAsync()
        };
}
