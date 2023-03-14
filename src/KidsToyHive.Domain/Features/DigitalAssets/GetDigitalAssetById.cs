using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DigitalAssets;

public class GetDigitalAssetById
{
    public class Request : IRequest<Response>
    {
        public Guid DigitalAssetId { get; set; }
    }
    public class Response
    {
        public DigitalAssetDto DigitalAsset { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;

        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                DigitalAsset = (await _context.DigitalAssets.FindAsync(request.DigitalAssetId)).ToDto()
            };
    }
}
