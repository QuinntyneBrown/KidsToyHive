using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Brands;

public class GetBrandsRequest : IRequest<GetBrandsResponse> { }
public class GetBrandsResponse
{
    public IEnumerable<BrandDto> Brands { get; set; }
}
public class GetBrandsHandler : IRequestHandler<GetBrandsRequest, GetBrandsResponse>
{
    private readonly IAppDbContext _context;
    public GetBrandsHandler(IAppDbContext context) => _context = context;
    public async Task<GetBrandsResponse> Handle(GetBrandsRequest request, CancellationToken cancellationToken)
        => new GetBrandsResponse()
        {
            Brands = await _context.Brands.Select(x => x.ToDto()).ToArrayAsync()
        };
}
