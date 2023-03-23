using KidsToyHive.Core.Interfaces;
using KidsToyHive.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DigitalAssets;

public class GetDigitalAssetByNameRequest : IRequest<GetDigitalAssetByNameResponse>
{
    public string Name { get; set; }
}
public class GetDigitalAssetByNameResponse
{
    public DigitalAssetDto DigitalAsset { get; set; }
}
public class GetDigitalAssetByNameHandler : IRequestHandler<GetDigitalAssetByNameRequest, GetDigitalAssetByNameResponse>
{
    private readonly IAppDbContext _context;
    private readonly ICache _cache;
    public GetDigitalAssetByNameHandler(IAppDbContext context, ICache cache)
    {
        _context = context;
        _cache = cache;
    }
    public async Task<GetDigitalAssetByNameResponse> Handle(GetDigitalAssetByNameRequest request, CancellationToken cancellationToken)
    {
        return new GetDigitalAssetByNameResponse()
        {
            DigitalAsset = (await _cache.FromCacheOrServiceAsync(() => _context.DigitalAssets.SingleAsync(x => x.Name == request.Name), request.Name)).ToDto()
        };
    }
}
