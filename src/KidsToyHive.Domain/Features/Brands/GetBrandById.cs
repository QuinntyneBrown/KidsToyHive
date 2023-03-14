using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Brands;

public class GetBrandByIdRequest : IRequest<GetBrandByIdResponse>
{
    public Guid BrandId { get; set; }
}
public class GetBrandByIdResponse
{
    public BrandDto Brand { get; set; }
}
public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdRequest, GetBrandByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetBrandByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetBrandByIdResponse> Handle(GetBrandByIdRequest request, CancellationToken cancellationToken)
        => new GetBrandByIdResponse()
        {
            Brand = (await _context.Brands.FindAsync(request.BrandId)).ToDto()
        };
}
