using KidsToyHive.Core.Interfaces;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DigitalAssets
{
    public class GetDigitalAssetByName
    {
        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
        }

        public class Response
        {
            public DigitalAssetDto DigitalAsset { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly ICache _cache;

            public Handler(IAppDbContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    DigitalAsset = (await _cache.FromCacheOrServiceAsync(() => _context.DigitalAssets.SingleAsync(x => x.Name == request.Name),request.Name)).ToDto()
                };
            }
        }
    }
}
